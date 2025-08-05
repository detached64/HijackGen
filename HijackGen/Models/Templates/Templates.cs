using System;
using System.IO;

namespace HijackGen.Models.Templates
{
    public abstract class Templates
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

    public sealed class HeaderTemplates : Templates
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

    public sealed class FunctionTemplates : Templates
    {
        public static readonly string GetAddress = GetTemplate("GetAddress");
        public static readonly string Free = GetTemplate("Free");
        public static readonly string Init = GetTemplate("Init");
        public static readonly string ExternX86 = GetTemplate("ExternX86");
        public static readonly string ExternX64 = GetTemplate("ExternX64");
        public static readonly string DllMainWithHijack = GetTemplate("DllMainWithHijack");
        public static readonly string DllMain = GetTemplate("DllMain");
    }

    public sealed class FileTemplates : Templates
    {
        public static readonly string Sln = GetTemplate("Sln");
        public static readonly string Proj = GetTemplate("Proj");
        public static readonly string ProjWithDef = GetTemplate("ProjWithDef");
    }
}
