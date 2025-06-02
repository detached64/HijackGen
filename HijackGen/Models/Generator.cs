using HijackGen.Models.Enums;
using HijackGen.Models.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HijackGen.Models
{
    public abstract class Generator : IDisposable
    {
        protected static string DllName;
        protected static List<DllExportInfo> Infos;
        protected static GenerationType Type;
        protected static PeArchitecture Architecture;

        protected static string HName => $"{DllName}.h";
        protected static string DefName => $"{DllName}.def";
        protected static string CName => "dllmain.c";
        protected static string CppName => "dllmain.cpp";
        protected static string SlnName => "Hijack.sln";
        protected static string ProjectName => $"{DllName}.vcxproj";

        public static Generator Create(string dllName, List<DllExportInfo> infos, GenerationType type, PeArchitecture architecture, GenerationFormat format)
        {
            DllName = dllName;
            Infos = infos.FindAll(item => !string.IsNullOrWhiteSpace(item.Name));
            Type = type;
            Architecture = architecture;
            return format switch
            {
                GenerationFormat.H => new HGenerator(),
                GenerationFormat.Def => new DefGenerator(),
                GenerationFormat.C => new CGenerator(),
                GenerationFormat.Cpp => new CppGenerator(),
                GenerationFormat.Sln => new SlnGenerator(),
                _ => throw new NotSupportedException(format.ToString()),
            };
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
            StringBuilder sb = new();
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
            switch (Type)
            {
                case GenerationType.System:
                    switch (Architecture)
                    {
                        case PeArchitecture.x64:
                            return GenerateX64();
                        case PeArchitecture.x86:
                            return GenerateX86();
                    }
                    break;
                case GenerationType.Custom:
                    return GenerateCustom();
            }
            return null;
        }

        private Dictionary<string, string> GenerateX86()
        {
            StringBuilder sb = new();
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
            return new Dictionary<string, string>
            {
                [HName] = GenerateHX64(),
                [DefName] = GenerateDefX64()
            };
        }

        private string GenerateHX64()
        {
            StringBuilder sb = new();
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
            StringBuilder sb = new();
            sb.AppendLine("LIBRARY").AppendLine("EXPORTS");
            foreach (DllExportInfo item in Infos)
            {
                sb.AppendLine($"{item.Name}=Redirect_{item.Name} @{item.Ordinal}");
            }
            return sb.ToString();
        }

        private Dictionary<string, string> GenerateCustom()
        {
            StringBuilder sb = new();
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
            if (files.TryGetValue(HName, out string value))
            {
                files[CppName] = GenerateCpp(value);
                files.Remove(HName);
            }
            return files;
        }

        private string GenerateCpp(string h_str)
        {
            StringBuilder sb = new();
            sb.AppendLine(h_str);
            //sb.AppendLine(IsSystemDll ? FunctionTemplates.DllMainWithHijack : FunctionTemplates.DllMain);
            sb.AppendLine(Type switch
            {
                GenerationType.System => FunctionTemplates.DllMainWithHijack,
                GenerationType.Custom => FunctionTemplates.DllMain,
                _ => throw new NotSupportedException(Type.ToString())
            });
            return sb.ToString();
        }
    }

    public sealed class CGenerator : CppGenerator
    {
        public override Dictionary<string, string> Generate()
        {
            Dictionary<string, string> files = base.Generate();
            if (files.TryGetValue(CppName, out string value))
            {
                files[CName] = value;
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
            StringBuilder sb = new();
            ProjectGUID = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";
            SolutionGUID = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";
            sb.AppendFormat(FileTemplates.Sln, CppProjectGUID, DllName, ProjectGUID, SolutionGUID);
            return sb.ToString();
        }

        private string GenerateProj()
        {
            StringBuilder sb = new();
            if (Architecture == PeArchitecture.x64 && Type == GenerationType.System)
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
            StringBuilder sb = new();
            sb.AppendLine(HeaderTemplates.BaseHeaders);
            sb.AppendFormat(HeaderTemplates.CustomHeaders, DllName).AppendLine().AppendLine();
            sb.AppendLine(Type switch
            {
                GenerationType.System => FunctionTemplates.DllMainWithHijack,
                GenerationType.Custom => FunctionTemplates.DllMain,
                _ => throw new NotSupportedException(Type.ToString())
            });
            return sb.ToString();
        }
    }
}
