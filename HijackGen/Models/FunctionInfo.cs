
namespace HijackGen.Models;

internal abstract class FunctionInfo
{
    public string Name { get; set; }
}

internal sealed class ExportInfo : FunctionInfo
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

internal sealed class ImportInfo : FunctionInfo
{
    public string DllName { get; set; }
    public ushort Hint { get; set; }
    public ulong IATOffset { get; set; }
}
