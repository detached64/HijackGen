using HijackGen.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HijackGen.Models.Generators;

internal class CMakeGenerator : HGenerator
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
        files["CMakeLists.txt"] = GenerateCMakeLists();
        return files;
    }

    private static string GenerateCMakeLists()
    {
        StringBuilder sb = new();
        sb.AppendLine("cmake_minimum_required(VERSION 3.10)");
        sb.AppendLine(Architecture switch
        {
            PeArchitecture.x64 => "set(CMAKE_GENERATOR_PLATFORM x64)",
            PeArchitecture.x86 => "set(CMAKE_GENERATOR_PLATFORM win32)",
            _ => throw new NotSupportedException(Architecture.ToString())
        });
        sb.AppendLine($"project({DllName} CXX)");
        sb.AppendLine("set(CMAKE_CXX_STANDARD 17)");
        sb.AppendLine("set(CMAKE_CXX_STANDARD_REQUIRED ON)");
        sb.AppendLine("set(CMAKE_CXX_EXTENSIONS OFF)");
        if (Architecture == PeArchitecture.x64 && Type == PeType.System)
        {
            sb.AppendLine($"set(hijack_src src/{CppName} src/{DefName})");
        }
        else
        {
            sb.AppendLine($"set(hijack_src src/{CppName})");
        }
        sb.AppendLine($"add_library({DllName} SHARED ${{hijack_src}})");
        sb.AppendLine($"target_include_directories({DllName} PUBLIC include)");
        return sb.ToString();
    }
}
