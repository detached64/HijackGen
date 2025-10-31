using System.Collections.Generic;
using System.Text;

namespace HijackGen.Models.Generators;

internal class DefGenerator : Generator
{
    public override Dictionary<string, string> Generate()
    {
        StringBuilder sb = new();
        sb.AppendLine("EXPORTS");
        foreach (ExportInfo item in Infos)
        {
            sb.AppendLine($"{item.Name}={DllName}.{item.Name} @{item.Ordinal}");
        }
        return new Dictionary<string, string> { { DefName, sb.ToString() } };
    }
}
