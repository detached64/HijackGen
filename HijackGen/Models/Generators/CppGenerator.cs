using HijackGen.Enums;
using HijackGen.Models.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace HijackGen.Models.Generators;

internal class CppGenerator : HGenerator
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

    private static string GenerateCpp(string h_str)
    {
        StringBuilder sb = new();
        sb.AppendLine(h_str);
        sb.AppendLine(Type switch
        {
            PeType.System => FunctionTemplates.DllMainWithHijack,
            PeType.Custom => FunctionTemplates.DllMain,
            _ => throw new NotSupportedException(Type.ToString())
        });
        return sb.ToString();
    }
}
