using HijackGen.Enums;
using HijackGen.Strings;
using PeNet;
using PeNet.Header.Pe;
using System;
using System.Collections.Generic;
using System.IO;

namespace HijackGen.Models;

internal sealed class PeParser : IDisposable
{
    private readonly string SystemDir32 = "C:\\Windows\\SysWOW64\\";
    private readonly string SystemDir64 = "C:\\Windows\\System32\\";

    public PeParser(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException(MsgStrings.EmptyPath);
        }
        if (!File.Exists(path))
        {
            throw new FileNotFoundException(string.Format(MsgStrings.NotFound, path));
        }
        PePath = path;
        Pe = new PeFile(path);
    }

    private readonly string PePath;
    private PeFile Pe;

    public bool IsSystem => PePath.StartsWith(SystemDir32, StringComparison.OrdinalIgnoreCase) ||
        PePath.StartsWith(SystemDir64, StringComparison.OrdinalIgnoreCase);
    public bool IsDll => Pe.IsDll;
    public PeType GenerationType => IsSystem ? PeType.System : PeType.Custom;
    public PeArchitecture Architecture => Pe.Is64Bit ? PeArchitecture.x64 : PeArchitecture.x86;

    public List<ExportInfo> GetExportInfos()
    {
        List<ExportInfo> items = [];
        if (Pe.ExportedFunctions is not null)
        {
            foreach (ExportFunction export in Pe.ExportedFunctions)
            {
                items.Add(new ExportInfo
                {
                    Ordinal = export.Ordinal,
                    Address = export.Address,
                    Name = export.Name,
                    HasForward = export.HasForward,
                    ForwardName = export.ForwardName
                });
            }
        }
        return items;
    }

    public List<ImportInfo> GetImportInfos()
    {
        List<ImportInfo> items = [];
        if (Pe.ImportedFunctions is not null)
        {
            foreach (ImportFunction import in Pe.ImportedFunctions)
            {
                items.Add(new ImportInfo
                {
                    IATOffset = import.IATOffset,
                    Name = import.Name,
                    DllName = import.DLL,
                    Hint = import.Hint
                });
            }
        }
        return items;
    }

    #region IDisposable
    private bool disposed = false;
    private void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }
        Pe = null;
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
