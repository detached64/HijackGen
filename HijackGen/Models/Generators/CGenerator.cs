using System.Collections.Generic;

namespace HijackGen.Models.Generators;

internal class CGenerator : CppGenerator
{
    public override Dictionary<string, string> Generate()
    {
        Dictionary<string, string> files = base.Generate();
        if (files.TryGetValue(CppName, out string value))
        {
            files[CName] = value;
            files.Remove(CppName);
        }
        return files;
    }
}
