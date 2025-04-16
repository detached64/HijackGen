using PeNet;
using System;
using System.Collections.Generic;
using System.IO;

namespace HijackGen
{
    public sealed class DllParser : IDisposable
    {
        private readonly string SystemDir32 = "C:\\Windows\\SysWOW64\\";
        private readonly string SystemDir64 = "C:\\Windows\\System32\\";

        public DllParser(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(Message.msgEmpty);
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(string.Format(Message.msgNotFound, path));
            }
            PEPath = path;
            PE = new PeFile(path);
            if (Type == PeType.Unknown)
            {
                throw new ArgumentException(string.Format(Message.msgNotDll, path));
            }
        }

        private readonly string PEPath;

        private PeFile PE;

        public bool IsX64 => PE.Is64Bit;

        public bool IsX86 => PE.Is32Bit;

        public bool IsSystemDll => IsSystem();

        public PeArchitecture Architecture => PE.Is64Bit ? PeArchitecture.x64 : PeArchitecture.x86;

        public PeType Type => PE.IsDll ? PeType.Dll : (PE.IsExe ? PeType.Exe : PeType.Unknown);

        public List<FunctionInfo> GetFuncInfos()
        {
            List<FunctionInfo> items = new List<FunctionInfo>();
            switch (Type)
            {
                case PeType.Dll:
                    if (PE.ExportedFunctions != null)
                    {
                        foreach (var export in PE.ExportedFunctions)
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
                    break;
                case PeType.Exe:
                    if (PE.ImportedFunctions != null)
                    {
                        foreach (var import in PE.ImportedFunctions)
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
                    break;
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

    public enum PeType
    {
        Unknown,
        Dll,
        Exe,
    }

    public enum PeArchitecture
    {
        x86,
        x64,
    }
}
