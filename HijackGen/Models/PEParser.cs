using HijackGen.Models.Enums;
using HijackGen.Strings;
using PeNet;
using PeNet.Header.Pe;
using System;
using System.Collections.Generic;
using System.IO;

namespace HijackGen.Models
{
    public abstract class PEParser;

    public sealed class ExportParser : PEParser, IDisposable
    {
        private readonly string SystemDir32 = "C:\\Windows\\SysWOW64\\";
        private readonly string SystemDir64 = "C:\\Windows\\System32\\";

        public ExportParser(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(Messages.msgPleaseInputPath);
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(string.Format(Messages.msgNotFound, path));
            }
            PEPath = path;
            PE = new PeFile(path);
            if (!PE.IsDll)
            {
                throw new ArgumentException(string.Format(Messages.msgNotDll, path));
            }
        }

        private readonly string PEPath;
        private PeFile PE;

        public GenerationType GenerationType => IsSystem() ? GenerationType.System : GenerationType.Custom;
        public PeArchitecture Architecture => PE.Is64Bit ? PeArchitecture.x64 : PeArchitecture.x86;

        public List<DllExportInfo> GetInfos()
        {
            List<DllExportInfo> items = [];
            if (PE.ExportedFunctions is not null)
            {
                foreach (ExportFunction export in PE.ExportedFunctions)
                {
                    items.Add(new DllExportInfo
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

        private bool IsSystem()
        {
            return PEPath.StartsWith(SystemDir32, StringComparison.OrdinalIgnoreCase) ||
                PEPath.StartsWith(SystemDir64, StringComparison.OrdinalIgnoreCase);
        }

        #region IDisposable
        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            PE = null;
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public sealed class ImportParser : PEParser, IDisposable
    {
        private readonly string PEPath;
        private PeFile PE;

        public ImportParser(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(Messages.msgPleaseInputPath);
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(string.Format(Messages.msgNotFound, path));
            }
            PEPath = path;
            PE = new PeFile(path);
            if (!PE.IsExe && !PE.IsDll)
            {
                throw new ArgumentException(Messages.msgInvalidPE);
            }
        }

        public List<ExeImportInfo> GetInfos()
        {
            List<ExeImportInfo> items = [];
            if (PE.ImportedFunctions is not null)
            {
                foreach (ImportFunction import in PE.ImportedFunctions)
                {
                    items.Add(new ExeImportInfo
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
            PE = null;
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
