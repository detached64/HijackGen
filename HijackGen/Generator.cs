using HijackGen.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace HijackGen
{
    public abstract class Generator : IDisposable
    {
        protected string DllName;
        protected List<DataItem> Items;

        public Generator(string dllName, List<DataItem> items)
        {
            DllName = dllName;
            Items = items.FindAll(item => !string.IsNullOrWhiteSpace(item.Name));   // Filter out empty names
        }

        public abstract Dictionary<FileProperty, string> Generate();

        #region IDisposable
        protected bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                Items.Clear();
                Items = null;
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
        public DefGenerator(string dll, List<DataItem> items) : base(dll, items) { }

        public override Dictionary<FileProperty, string> Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("EXPORTS");
            foreach (DataItem item in Items)
            {
                sb.AppendLine($"{item.Name}={DllName}.{item.Name} @{item.Ordinal}");
            }
            return new Dictionary<FileProperty, string> { { FileProperty.Def, sb.ToString() } };
        }
    }

    public sealed class HGenerator : Generator
    {
        private readonly bool IsSystemDll;
        private readonly bool IsX64;
        private readonly bool GenDefX64;

        public HGenerator(string dll, List<DataItem> items, bool isSystemDll, bool isX64, bool genDefX64) : base(dll, items) { IsSystemDll = isSystemDll; IsX64 = isX64; GenDefX64 = genDefX64; }

        public override Dictionary<FileProperty, string> Generate()
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

        private Dictionary<FileProperty, string> GenerateX86()
        {
            StringBuilder sb = new StringBuilder();
            // Header, includes, and linker comments
            sb.AppendLine(HeaderTemplates.BaseHeaders).AppendLine();
            foreach (DataItem item in Items)
            {
                sb.AppendFormat(HeaderTemplates.LinkerComment, item.Name, HeaderTemplates.Redirect, item.Name, item.Ordinal).AppendLine();
            }
            sb.AppendLine();
            // Real function & dll declarations
            foreach (DataItem item in Items)
            {
                sb.AppendFormat(HeaderTemplates.RealFuncX86, item.Name).AppendLine();
            }
            sb.AppendLine();
            sb.AppendLine(HeaderTemplates.RealDll).AppendLine();
            // GetAddress function
            sb.AppendFormat(FunctionTemplates.GetAddress, DllName).AppendLine();
            // Free function
            sb.AppendLine(FunctionTemplates.Free);
            // Init funcion
            sb.AppendFormat(FunctionTemplates.Init, DllName);
            foreach (DataItem item in Items)
            {
                sb.Append(HeaderTemplates.Tab).AppendFormat(HeaderTemplates.InitRealFunc, item.Name).AppendLine();
            }
            sb.AppendLine("}").AppendLine();
            // Extern functions
            foreach (DataItem item in Items)
            {
                sb.AppendFormat(FunctionTemplates.ExternX86, item.Name).AppendLine();
            }
            return new Dictionary<FileProperty, string> { { FileProperty.Header, sb.ToString() } };
        }

        private Dictionary<FileProperty, string> GenerateX64()
        {
            Dictionary<FileProperty, string> files = new Dictionary<FileProperty, string>();
            files[FileProperty.Header] = GenerateHX64();
            if (GenDefX64)
            {
                files[FileProperty.Def] = GenerateDefX64();
            }
            return files;
        }

        private string GenerateHX64()
        {
            StringBuilder sb = new StringBuilder();
            // Header and includes
            sb.AppendLine(HeaderTemplates.BaseHeaders).AppendLine();
            // Real function & dll declarations
            foreach (DataItem item in Items)
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
            foreach (DataItem item in Items)
            {
                sb.Append(HeaderTemplates.Tab).AppendFormat(HeaderTemplates.InitRealFunc, item.Name).AppendLine();
            }
            sb.AppendLine("}").AppendLine();
            // Extern functions
            foreach (DataItem item in Items)
            {
                sb.AppendFormat(FunctionTemplates.ExternX64, item.Name).AppendLine();
            }
            return sb.ToString();
        }

        private string GenerateDefX64()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("LIBRARY").AppendLine("EXPORTS");
            foreach (DataItem item in Items)
            {
                sb.AppendLine($"{item.Name}=Redirect_{item.Name} @{item.Ordinal}");
            }
            return sb.ToString();
        }

        private Dictionary<FileProperty, string> GenerateCustom()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataItem item in Items)
            {
                sb.AppendFormat(HeaderTemplates.LinkerComment, item.Name, DllName + ".", item.Name, item.Ordinal).AppendLine();
            }
            return new Dictionary<FileProperty, string> { { FileProperty.Header, sb.ToString() } };
        }
    }

    public enum FileProperty
    {
        Def,
        Header
    }
}
