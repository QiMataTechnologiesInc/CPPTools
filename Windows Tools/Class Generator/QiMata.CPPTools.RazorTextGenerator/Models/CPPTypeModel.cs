using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiMata.CPPTools.RazorTextGenerator.Models
{
    public class CPPTypeModel
    {
        public CPPTypeModel()
        {
            Namespaces = new List<string>();
            Properties = new List<Property>();

        }

        public IEnumerable<string> Namespaces { get; set; }

        public string ClassName { get; set; }

        public IEnumerable<Property> Properties { get; set; }

        public IEnumerable<string> DirectInheritanceTypes { get; set; }

        public IEnumerable<string> TemplateParameters { get; set; }

        public IEnumerable<Method> Methods { get; set; }

    }
}
