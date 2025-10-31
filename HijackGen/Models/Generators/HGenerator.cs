using HijackGen.Enums;
using HijackGen.Models.Templates;
using System.Collections.Generic;
using System.Text;

namespace HijackGen.Models.Generators;

internal class HGenerator : Generator
{
    public override Dictionary<string, string> Generate()
    {
        switch (Type)
        {
            case PeType.System:
                switch (Architecture)
                {
                    case PeArchitecture.x64:
                        return GenerateX64();
                    case PeArchitecture.x86:
                        return GenerateX86();
                }
                break;
            case PeType.Custom:
                return GenerateCustom();
        }
        return null;
    }

    private static Dictionary<string, string> GenerateX86()
    {
        StringBuilder sb = new();
        // Header, includes, and linker comments
        sb.AppendLine(HeaderTemplates.BaseHeaders).AppendLine();
        foreach (ExportInfo item in Infos)
        {
            sb.AppendFormat(HeaderTemplates.LinkerComment, item.Name, HeaderTemplates.Redirect, item.Name, item.Ordinal).AppendLine();
        }
        sb.AppendLine();
        // Real function & dll declarations
        foreach (ExportInfo item in Infos)
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
        foreach (ExportInfo item in Infos)
        {
            sb.Append(HeaderTemplates.Tab).AppendFormat(HeaderTemplates.InitRealFunc, item.Name).AppendLine();
        }
        sb.AppendLine("}").AppendLine();
        // Extern functions
        foreach (ExportInfo item in Infos)
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

    private static string GenerateHX64()
    {
        StringBuilder sb = new();
        // Header and includes
        sb.AppendLine(HeaderTemplates.BaseHeaders).AppendLine();
        // Real function & dll declarations
        foreach (ExportInfo item in Infos)
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
        foreach (ExportInfo item in Infos)
        {
            sb.Append(HeaderTemplates.Tab).AppendFormat(HeaderTemplates.InitRealFunc, item.Name).AppendLine();
        }
        sb.AppendLine("}").AppendLine();
        // Extern functions
        foreach (ExportInfo item in Infos)
        {
            sb.AppendFormat(FunctionTemplates.ExternX64, item.Name).AppendLine();
        }
        return sb.ToString();
    }

    private static string GenerateDefX64()
    {
        StringBuilder sb = new();
        sb.AppendLine("LIBRARY").AppendLine("EXPORTS");
        foreach (ExportInfo item in Infos)
        {
            sb.AppendLine($"{item.Name}=Redirect_{item.Name} @{item.Ordinal}");
        }
        return sb.ToString();
    }

    private static Dictionary<string, string> GenerateCustom()
    {
        StringBuilder sb = new();
        sb.AppendLine(HeaderTemplates.BaseHeaders);
        foreach (ExportInfo item in Infos)
        {
            sb.AppendFormat(HeaderTemplates.LinkerComment, item.Name, DllName + ".", item.Name, item.Ordinal).AppendLine();
        }
        return new Dictionary<string, string> { { HName, sb.ToString() } };
    }
}
