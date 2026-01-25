using System;
using System.IO;

namespace HijackGen.Models.Templates;

internal abstract class Templates
{
    protected static string GetTemplate(string name)
    {
        string path = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Templates",
            $"{name}.txt");
        if (File.Exists(path))
        {
            using FileStream stream = File.OpenRead(path);
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
        throw new FileNotFoundException($"Template file '{name}.txt' not found in Templates directory.", path);
    }
}

internal sealed class HeaderTemplates : Templates
{
    public static readonly string Tab = "    ";
    public static readonly string BaseHeaders =
        "#include <Windows.h>";
    public static readonly string SystemDllPath =
        $"{Environment.GetFolderPath(Environment.SpecialFolder.System)}" + @"\{0}.dll";
    public static readonly string Redirect =
        "_Redirect_";
    public static readonly string LinkerComment =
        @"#pragma comment(linker, ""/EXPORT:{0}={1}{2},@{3}"")";
    public static readonly string RealFuncX86 =
        "PVOID Real_{0} = NULL;";
    public static readonly string RealFuncX64 =
        "FARPROC Real_{0} = NULL;";
    public static readonly string RealDll =
        "HMODULE Real_Module = NULL;";
    public static readonly string InitRealDll =
        @"Real_Module = LoadLibrary(""{0}"");";
    public static readonly string InitRealFunc =
        @"Real_{0} = GetAddress(""{0}"");";
    public static readonly string CustomHeaders =
        @"#include ""{0}.h""";
}

internal sealed class FunctionTemplates : Templates
{
    public static readonly string GetAddress = GetTemplate(nameof(GetAddress));
    public static readonly string Free = GetTemplate(nameof(Free));
    public static readonly string Init = GetTemplate(nameof(Init));
    public static readonly string ExternX86 = GetTemplate(nameof(ExternX86));
    public static readonly string ExternX64 = GetTemplate(nameof(ExternX64));
    public static readonly string DllMainWithHijack = GetTemplate(nameof(DllMainWithHijack));
    public static readonly string DllMain = GetTemplate(nameof(DllMain));
}

internal sealed class FileTemplates : Templates
{
    public static readonly string Sln = GetTemplate(nameof(Sln));
    public static readonly string Project = GetTemplate(nameof(Project));
    public static readonly string ProjectWithDef = GetTemplate(nameof(ProjectWithDef));
    public static readonly string Gitignore = GetTemplate(nameof(Gitignore));
}
