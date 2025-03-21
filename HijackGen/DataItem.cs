namespace HijackGen
{
    public sealed class DataItem
    {
        public int Ordinal { get; set; }
        public ulong Address { get; set; }
        public string Name { get; set; }
        public bool HasForward { get; set; }

        public string ForwardName
        {
            get => HasForward ? _forwardName : null;
            set => _forwardName = value;
        }
        private string _forwardName;
    }
}
