using HijackGen.Enums;
using HijackGen.Models.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HijackGen.Models.Generators;

internal class SolutionGenerator : HGenerator
{
    private const string CppProjectGUID = "{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}";
    private string ProjectGUID;
    private string SolutionGUID;

    public override Dictionary<string, string> Generate()
    {
        Dictionary<string, string> files = base.Generate().ToDictionary(kvp => $"{DllName}\\{kvp.Key}", kvp => kvp.Value);
        files[$"{SlnName}"] = GenerateSolution();
        files[$"{DllName}\\{ProjectName}"] = GenerateProject();
        files[$"{DllName}\\{CppName}"] = GenerateCpp();
        return files;
    }

    private string GenerateSolution()
    {
        StringBuilder sb = new();
        ProjectGUID = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";
        SolutionGUID = $"{{{Guid.NewGuid().ToString().ToUpper()}}}";
        sb.AppendFormat(FileTemplates.Solution, CppProjectGUID, DllName, ProjectGUID, SolutionGUID);
        return sb.ToString();
    }

    private string GenerateProject()
    {
        StringBuilder sb = new();
        if (Architecture is PeArchitecture.x64 && Type is PeType.System)
        {
            sb.AppendFormat(FileTemplates.ProjectWithDef, ProjectGUID, DllName, DefName);
        }
        else
        {
            sb.AppendFormat(FileTemplates.Project, ProjectGUID, DllName);
        }
        return sb.ToString();
    }
}
