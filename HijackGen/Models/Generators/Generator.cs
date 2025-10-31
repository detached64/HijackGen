using HijackGen.Enums;
using HijackGen.Models.Templates;
using System;
using System.Collections.Generic;
using System.Text;

namespace HijackGen.Models.Generators;

internal abstract class Generator : IDisposable
{
    protected static string DllName;
    protected static List<ExportInfo> Infos;
    protected static PeType Type;
    protected static PeArchitecture Architecture;

    protected static string HName => $"{DllName}.h";
    protected static string DefName => $"{DllName}.def";
    protected static string CName => "dllmain.c";
    protected static string CppName => "dllmain.cpp";
    protected static string SlnName => "Hijack.sln";
    protected static string ProjectName => $"{DllName}.vcxproj";

    public static Generator Create(string dllName, List<ExportInfo> infos, PeType type, PeArchitecture architecture, GenerationFormat format)
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
            GenerationFormat.Solution => new SolutionGenerator(),
            GenerationFormat.CMake => new CMakeGenerator(),
            GenerationFormat.Xmake => new XmakeGenerator(),
            _ => throw new NotSupportedException(format.ToString()),
        };
    }

    public abstract Dictionary<string, string> Generate();

    protected static string GenerateCpp()
    {
        StringBuilder sb = new();
        sb.AppendLine(HeaderTemplates.BaseHeaders);
        sb.AppendFormat(HeaderTemplates.CustomHeaders, DllName).AppendLine().AppendLine();
        sb.AppendLine(Type switch
        {
            PeType.System => FunctionTemplates.DllMainWithHijack,
            PeType.Custom => FunctionTemplates.DllMain,
            _ => throw new NotSupportedException(Type.ToString())
        });
        return sb.ToString();
    }

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
