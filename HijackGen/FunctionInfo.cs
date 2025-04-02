namespace HijackGen
{
    public abstract class FunctionInfo
    {
        public string Name { get; set; }
    }

    public sealed class DllExportInfo : FunctionInfo
    {
        public int Ordinal { get; set; }
        public ulong Address { get; set; }
        public bool HasForward { get; set; }

        public string ForwardName
        {
            get => HasForward ? _forwardName : null;
            set => _forwardName = value;
        }
        private string _forwardName;
    }

    public sealed class ExeImportInfo : FunctionInfo
    {
        public string DllName { get; set; }
        public ushort Hint { get; set; }
        public ulong IATOffset { get; set; }
    }
}
