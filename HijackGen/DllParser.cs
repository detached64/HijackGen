using PeNet;
using System;
using System.Collections.Generic;
using System.IO;

namespace HijackGen
{
    public sealed class DllParser : IDisposable
    {
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
            Path = path;
            Pe = new PeFile(path);
            if (Type == PeType.Unknown)
            {
                throw new ArgumentException(string.Format(Message.msgNotDll, path));
            }
        }

        private readonly string Path;

        private PeFile Pe;

        public PeArchitecture Architecture => Pe.Is64Bit ? PeArchitecture.x64 : PeArchitecture.x86;

        public PeType Type => Pe.IsDll ? PeType.Dll : (Pe.IsExe ? PeType.Exe : PeType.Unknown);

        public List<FunctionInfo> GetFuncInfos()
        {
            List<FunctionInfo> items = new List<
                FunctionInfo>();
            switch (Type)
            {
                case PeType.Dll:
                    if (Pe.ExportedFunctions != null)
                    {
                        foreach (var export in Pe.ExportedFunctions)
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
                    if (Pe.ExportedFunctions != null)
                    {
                        foreach (var import in Pe.ImportedFunctions)
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
