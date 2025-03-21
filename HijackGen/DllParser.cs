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
            Dll = new PeFile(path);
            if (!Dll.IsDll)
            {
                throw new ArgumentException(string.Format(Message.msgNotDll, path));
            }
            if (Dll.ExportedFunctions == null)
            {
                throw new ArgumentException(string.Format(Message.msgNoExport, path));
            }
        }

        public string Path { get; private set; }

        public PeFile Dll { get; private set; }

        public List<DataItem> GetExportInfos()
        {
            List<DataItem> items = new List<DataItem>();
            foreach (var export in Dll.ExportedFunctions)
            {
                items.Add(new DataItem
                {
                    Ordinal = export.Ordinal,
                    Address = export.Address,
                    Name = export.Name,
                    HasForward = export.HasForward,
                    ForwardName = export.ForwardName
                });
            }
            return items;
        }

        public void Dispose()
        {
            Dll = null;
        }
    }
}
