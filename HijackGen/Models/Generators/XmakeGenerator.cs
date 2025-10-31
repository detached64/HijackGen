using HijackGen.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HijackGen.Models.Generators;

internal class XmakeGenerator : HGenerator
{
    public override Dictionary<string, string> Generate()
    {
        Dictionary<string, string> files = base.Generate().ToDictionary(kvp =>
        {
            string key = kvp.Key;
            return key.EndsWith(".def", StringComparison.OrdinalIgnoreCase)
                ? "src\\" + key
                : key.EndsWith(".h", StringComparison.OrdinalIgnoreCase)
                ? "include\\" + key
                : throw new InvalidOperationException($"Unexpected file type: {key}");
        }, kvp => kvp.Value);
        files[$"src\\{CppName}"] = GenerateCpp();
        files["xmake.lua"] = GenerateXmakeLua();
        return files;
    }

    private static string GenerateXmakeLua()
    {
        StringBuilder sb = new();
        sb.AppendLine($"set_project(\"{DllName}\")");
        sb.AppendLine("set_languages(\"c++17\")");
        sb.AppendLine("add_rules(\"mode.debug\", \"mode.release\")");
        sb.AppendLine(Architecture switch
        {
            PeArchitecture.x64 => "set_arch(\"x64\")",
            PeArchitecture.x86 => "set_arch(\"x86\")",
            _ => throw new NotSupportedException(Architecture.ToString())
        });
        sb.AppendLine("set_toolchains(\"msvc\")");
        sb.AppendLine($"target(\"{DllName}\")");
        sb.AppendLine("\tset_kind(\"shared\")");
        sb.AppendLine($"\tadd_files(\"src/{CppName}\")");
        if (Architecture == PeArchitecture.x64 && Type == PeType.System)
        {
            sb.AppendLine($"\tadd_files(\"src/{DefName}\")");
        }
        sb.AppendLine("\tadd_includedirs(\"include\")");
        sb.AppendLine("\tadd_links(\"user32\")");
        sb.AppendLine("\tadd_syslinks(\"user32\")");
        return sb.ToString();
    }
}
