using HijackGen.Templates;
using System.Collections.Generic;
using System.Text;

namespace HijackGen
{
    public class Generator
    {
        public string DllName;
        public List<DataItem> Items;

        public Generator(string dllName, List<DataItem> items)
        {
            DllName = dllName;
            Items = items.FindAll(item => !string.IsNullOrWhiteSpace(item.Name));   // Filter out empty names
        }
    }

    public sealed class DefGenerator : Generator
    {
        public DefGenerator(string dll, List<DataItem> items) : base(dll, items) { }

        public string Generate()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("EXPORTS");
            foreach (DataItem item in Items)
            {
                sb.AppendLine($"{item.Name}={DllName}.{item.Name} @{item.Ordinal}");
            }
            return sb.ToString();
        }
    }

    public sealed class HGenerator : Generator
    {
        public HGenerator(string dll, List<DataItem> items) : base(dll, items) { }

        public string Generate()
        {
            StringBuilder sb = new StringBuilder();
            // Header, includes, and linker comments
            sb.AppendLine(HeaderTemplates.BaseHeaders).AppendLine();
            foreach (DataItem item in Items)
            {
                sb.AppendFormat(HeaderTemplates.LinkerComment, item.Name, "Redirect_" + item.Name, item.Ordinal).AppendLine();
            }
            sb.AppendLine();
            // Real function & dll declarations
            foreach (DataItem item in Items)
            {
                sb.AppendFormat(HeaderTemplates.RealFunc, item.Name).AppendLine();
            }
            sb.AppendLine();
            sb.AppendLine(HeaderTemplates.RealDll).AppendLine();
            // GetAddress function
            sb.AppendFormat(FunctionTemplates.GetAddress, DllName).AppendLine();
            // Free function
            sb.AppendLine(FunctionTemplates.Free);
            // Init funcion
            sb.AppendFormat(FunctionTemplates.Init, DllName);
            foreach (DataItem item in Items)
            {
                sb.Append(HeaderTemplates.Tab).AppendFormat(HeaderTemplates.InitRealFunc, item.Name).AppendLine();
            }
            sb.AppendLine("}").AppendLine();
            // Extern functions
            foreach (DataItem item in Items)
            {
                sb.AppendFormat(FunctionTemplates.Extern, item.Name).AppendLine();
            }
            return sb.ToString();
        }
    }
}
