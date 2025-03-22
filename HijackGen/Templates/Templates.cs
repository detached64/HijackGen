using System;
using System.IO;
using System.Reflection;

namespace HijackGen.Templates
{
    public class Templates
    {
        protected static string GetTemplate(string name)
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"HijackGen.Templates.{name}.txt"))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException($"Template {name} not found.");
                }
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }

    public sealed class HeaderTemplates : Templates
    {
        public static string Tab = "    ";
        public static string BaseHeaders =
            @"#include <windows.h>";
        public static string SystemDllPath =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.System)}" + @"\{0}.dll";
        public static string LinkerComment =
            @"#pragma comment(linker, ""/EXPORT:{0}=_{1},@{2}"")";
        public static string RealFunc =
            @"PVOID Real_{0} = NULL;";
        public static string RealDll =
            @"HMODULE Real_Module = NULL;";
        public static string InitRealDll =
            @"Real_Module = LoadLibrary(""{0}"");";
        public static string InitRealFunc =
            @"Real_{0} = GetAddress(""{0}"");";
    }

    public sealed class FunctionTemplates : Templates
    {
        public static string GetAddress = GetTemplate("GetAddress");
        public static string Free = GetTemplate("Free");
        public static string Init = GetTemplate("Init");
        public static string Extern = GetTemplate("Extern");
    }
}
