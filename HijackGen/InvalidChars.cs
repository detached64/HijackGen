using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HijackGen
{
    public static class InvalidChars
    {
        private readonly static string FilePath = "InvalidChars.txt";
        private readonly static char[] DefaultInvalidCharList = new char[] { '?', '@', '$' };
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading invalid characters: {ex.Message}");
                    UseDefaultThisTime = true;
                    return DefaultInvalidCharList;
                }
            }
        }

        private static char[] TryGetChars()
        {
            return File.ReadAllLines(FilePath)
                .Where(line => line.Length >= 1)
                .Select(line => line[0])
                .ToArray();
        }

        private static void WriteDefaultChars()
        {
            File.WriteAllLines(FilePath, DefaultInvalidCharList.Select(c => c.ToString()));
        }
    }
}
