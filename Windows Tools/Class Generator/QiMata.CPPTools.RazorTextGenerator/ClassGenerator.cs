using QiMata.CPPTools.RazorTextGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QiMata.CPPTools.RazorTextGenerator.Templates;
using RazorEngine;
using RazorEngine.Templating;

namespace QiMata.CPPTools.RazorTextGenerator
{
    public class ClassGenerator
    {
        public string GenerateClass(CPPTypeModel typeModel)
        {
            return Engine.Razor.RunCompile(EmbeddedResources.ClassHeaderTemplate, "ClassHeaderTemplate", null,typeModel);
        }
    }
}
