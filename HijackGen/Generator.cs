using HijackGen.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace HijackGen
{
    public abstract class Generator : IDisposable
    {
        protected string DllName;
        protected List<DllExportInfo> Infos;

        protected Generator(string dllName, List<DllExportInfo> infos)
        {
            DllName = dllName;
            Infos = infos.FindAll(item => !string.IsNullOrWhiteSpace(item.Name));   // Filter out empty names
        }

        public abstract Dictionary<FileType, string> Generate();

        #region IDisposable
        protected bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                Infos.Clear();
                Infos = null;
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public sealed class DefGenerator : Generator
    {
        public DefGenerator(string dll, List<DllExportInfo> infos) : base(dll, infos) { }

        public override Dictionary<FileType, string> Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("EXPORTS");
            foreach (DllExportInfo item in Infos)
            {
                sb.AppendLine($"{item.Name}={DllName}.{item.Name} @{item.Ordinal}");
            }
            return new Dictionary<FileType, string> { { FileType.Def, sb.ToString() } };
        }
    }

    public sealed class HGenerator : Generator
    {
        private readonly bool IsSystemDll;
        private readonly bool IsX64;

        public HGenerator(string dll, List<DllExportInfo> infos, bool isSystemDll, bool isX64) : base(dll, infos) { IsSystemDll = isSystemDll; IsX64 = isX64; }

        public override Dictionary<FileType, string> Generate()
        {
            if (IsSystemDll)
            {
                if (IsX64)
                {
                    return GenerateX64();
                }
                else
                {
                    return GenerateX86();
                }
            }
            else
            {
                return GenerateCustom();
            }
        }

        private Dictionary<FileType, string> GenerateX86()
        {
            StringBuilder sb = new StringBuilder();
            // Header, includes, and linker comments
            sb.AppendLine(HeaderTemplates.BaseHeaders).AppendLine();
            foreach (DllExportInfo item in Infos)
            {
                sb.AppendFormat(HeaderTemplates.LinkerComment, item.Name, HeaderTemplates.Redirect, item.Name, item.Ordinal).AppendLine();
            }
            sb.AppendLine();
            // Real function & dll declarations
            foreach (DllExportInfo item in Infos)
            {
                sb.AppendFormat(HeaderTemplates.RealFuncX86, item.Name).AppendLine();
            }
            sb.AppendLine();
            sb.AppendLine(HeaderTemplates.RealDll).AppendLine();
            // GetAddress function
            sb.AppendFormat(FunctionTemplates.GetAddress, DllName).AppendLine();
            // Free function
            sb.AppendFormat(FunctionTemplates.Free).AppendLine();
            // Init funcion
            sb.AppendFormat(FunctionTemplates.Init, DllName);
            foreach (DllExportInfo item in Infos)
            {
                sb.Append(HeaderTemplates.Tab).AppendFormat(HeaderTemplates.InitRealFunc, item.Name).AppendLine();
            }
            sb.AppendLine("}").AppendLine();
            // Extern functions
            foreach (DllExportInfo item in Infos)
            {
                sb.AppendFormat(FunctionTemplates.ExternX86, item.Name).AppendLine();
            }
            return new Dictionary<FileType, string> { { FileType.Header, sb.ToString() } };
        }

        private Dictionary<FileType, string> GenerateX64()
        {
            Dictionary<FileType, string> files = new Dictionary<FileType, string>();
            files[FileType.Header] = GenerateHX64();
            files[FileType.Def] = GenerateDefX64();
            return files;
        }

        private string GenerateHX64()
        {
            StringBuilder sb = new StringBuilder();
            // Header and includes
            sb.AppendLine(HeaderTemplates.BaseHeaders).AppendLine();
            // Real function & dll declarations
            foreach (DllExportInfo item in Infos)
            {
                sb.AppendFormat(HeaderTemplates.RealFuncX64, item.Name).AppendLine();
            }
            sb.AppendLine();
            sb.AppendLine(HeaderTemplates.RealDll).AppendLine();
            // GetAddress function
            sb.AppendFormat(FunctionTemplates.GetAddress, DllName).AppendLine();
            // Free function
            sb.AppendLine(FunctionTemplates.Free);
            // Init funcion
            sb.AppendFormat(FunctionTemplates.Init, DllName);
            foreach (DllExportInfo item in Infos)
            {
                sb.Append(HeaderTemplates.Tab).AppendFormat(HeaderTemplates.InitRealFunc, item.Name).AppendLine();
            }
            sb.AppendLine("}").AppendLine();
            // Extern functions
            foreach (DllExportInfo item in Infos)
            {
                sb.AppendFormat(FunctionTemplates.ExternX64, item.Name).AppendLine();
            }
            return sb.ToString();
        }

        private string GenerateDefX64()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("LIBRARY").AppendLine("EXPORTS");
            foreach (DllExportInfo item in Infos)
            {
                sb.AppendLine($"{item.Name}=Redirect_{item.Name} @{item.Ordinal}");
            }
            return sb.ToString();
        }

        private Dictionary<FileType, string> GenerateCustom()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DllExportInfo item in Infos)
            {
                sb.AppendFormat(HeaderTemplates.LinkerComment, item.Name, DllName + ".", item.Name, item.Ordinal).AppendLine();
            }
            return new Dictionary<FileType, string> { { FileType.Header, sb.ToString() } };
        }
    }

    public enum FileType
    {
        Def,
        Header
    }
}
