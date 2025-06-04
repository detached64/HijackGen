namespace HijackGen.Messengers
{
    public sealed class StatusBarMessage(string content)
    {
        public string Content { get; } = content;
    }
}
