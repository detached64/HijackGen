using HijackGen.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HijackGen
{
    public abstract class Generator : IDisposable
    {
        protected static string DllName;
        protected static List<DllExportInfo> Infos;
        protected static bool IsSystemDll;
        protected static bool IsX64;

        protected string HName => $"{DllName}.h";
        protected string DefName => $"{DllName}.def";
        protected string CName => "dllmain.c";
        protected string CppName => "dllmain.cpp";
        protected string SlnName => "Hijack.sln";
        protected string ProjectName => $"{DllName}.vcxproj";

        public static Generator Create(string dllName, List<DllExportInfo> infos, bool isSystem, bool isX64, string format)
        {
            DllName = dllName;
            Infos = infos.FindAll(item => !string.IsNullOrWhiteSpace(item.Name));
            IsSystemDll = isSystem;
            IsX64 = isX64;
            switch (format)
            {
                case "h":
                    return new HGenerator();
                case "def":
                    return new DefGenerator();
                case "c":
                    return new CGenerator();
                case "cpp":
                    return new CppGenerator();
                case "sln":
                    return new SlnGenerator();
                default:
                    throw new NotSupportedException(format);
            }
        }

        public abstract Dictionary<string, string> Generate();

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
        public override Dictionary<string, string> Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("EXPORTS");
            foreach (DllExportInfo item in Infos)
            {
                sb.AppendLine($"{item.Name}={DllName}.{item.Name} @{item.Ordinal}");
            }
            return new Dictionary<string, string> { { DefName, sb.ToString() } };
        }
    }

    public class HGenerator : Generator
    {
        public override Dictionary<string, string> Generate()
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

        private Dictionary<string, string> GenerateX86()
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
            return new Dictionary<string, string> { { HName, sb.ToString() } };
        }

        private Dictionary<string, string> GenerateX64()
        {
            Dictionary<string, string> files = new Dictionary<string, string>();
            files[HName] = GenerateHX64();
            files[DefName] = GenerateDefX64();
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

        private Dictionary<string, string> GenerateCustom()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(HeaderTemplates.BaseHeaders);
            foreach (DllExportInfo item in Infos)
            {
                sb.AppendFormat(HeaderTemplates.LinkerComment, item.Name, DllName + ".", item.Name, item.Ordinal).AppendLine();
            }
            return new Dictionary<string, string> { { HName, sb.ToString() } };
        }
    }

    public class CppGenerator : HGenerator
    {
        public override Dictionary<string, string> Generate()
        {
            Dictionary<string, string> files = base.Generate();
            if (files.ContainsKey(HName))
            {
                files[CppName] = GenerateCpp(files[HName]);
                files.Remove(HName);
            }
            return files;
        }

        private string GenerateCpp(string h_str)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(h_str);
            sb.AppendLine(IsSystemDll ? FunctionTemplates.DllMainWithHijack : FunctionTemplates.DllMain);
            return sb.ToString();
        }
    }

    public sealed class CGenerator : CppGenerator
    {
        public override Dictionary<string, string> Generate()
        {
            Dictionary<string, string> files = base.Generate();
            if (files.ContainsKey(CppName))
            {
                files[CName] = files[CppName];
                files.Remove(CppName);
            }
            return files;
        }
    }

    public sealed class SlnGenerator : HGenerator
    {
        private const string CppProjectGUID = "{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}";
        private string ProjectGUID;
        private string SolutionGUID;

        public override Dictionary<string, string> Generate()
        {
            Dictionary<string, string> files = base.Generate().ToDictionary(kvp => $"Hijack\\{DllName}\\{kvp.Key}", kvp => kvp.Value);
            files[$"Hijack\\{SlnName}"] = GenerateSln();
            files[$"Hijack\\{DllName}\\{ProjectName}"] = GenerateProj();
            files[$"Hijack\\{DllName}\\{CppName}"] = GenerateCpp();
            return files;
        }

        private string GenerateSln()
        {
            StringBuilder sb = new StringBuilder();
            ProjectGUID = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";
            SolutionGUID = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";
            sb.AppendFormat(FileTemplates.Sln, CppProjectGUID, DllName, ProjectGUID, SolutionGUID);
            return sb.ToString();
        }

        private string GenerateProj()
        {
            StringBuilder sb = new StringBuilder();
            if (IsX64 && IsSystemDll)
            {
                sb.AppendFormat(FileTemplates.ProjWithDef, ProjectGUID, DllName, DefName);
            }
            else
            {
                sb.AppendFormat(FileTemplates.Proj, ProjectGUID, DllName);
            }
            return sb.ToString();
        }

        private string GenerateCpp()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(HeaderTemplates.BaseHeaders);
            sb.AppendFormat(HeaderTemplates.CustomHeaders, DllName).AppendLine().AppendLine();
            sb.AppendLine(IsSystemDll ? FunctionTemplates.DllMainWithHijack : FunctionTemplates.DllMain);
            return sb.ToString();
        }
    }
}
