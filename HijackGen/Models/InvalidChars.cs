using System.IO;
using System.Linq;

namespace HijackGen.Models;

public static class InvalidChars
{
    private const string FilePath = "InvalidChars.txt";
    private readonly static char[] DefaultInvalidCharList = ['?', '@', '$'];
    private static bool UseDefaultThisTime = false;

    public static char[] InvalidCharList
    {
        get
        {
            try
            {
                if (UseDefaultThisTime)
                {
                    return DefaultInvalidCharList;
                }
                else
                {
                    if (!File.Exists(FilePath))
                    {
                        WriteDefaultChars();
                        return DefaultInvalidCharList;
                    }
                    return TryGetChars() ?? DefaultInvalidCharList;
                }
            }
            catch
            {
                UseDefaultThisTime = true;
                return DefaultInvalidCharList;
            }
        }
    }

    private static char[] TryGetChars()
    {
        return [.. File.ReadAllLines(FilePath)
                .Where(line => line.Length >= 1)
                .Select(line => line[0])];
    }

    private static void WriteDefaultChars()
    {
        File.WriteAllLines(FilePath, DefaultInvalidCharList.Select(c => c.ToString()));
    }
}
