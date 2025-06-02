namespace HijackGen.Messengers
{
    internal sealed class StatusBarMessage(string content)
    {
        public string Content { get; } = content;
    }
}
