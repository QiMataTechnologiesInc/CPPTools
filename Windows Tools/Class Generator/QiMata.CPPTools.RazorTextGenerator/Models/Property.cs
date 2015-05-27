using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiMata.CPPTools.RazorTextGenerator.Models
{
    public class Property
    {
        public string TypeName { get; set; }

        public string Name { get; set; }

        public bool HasGetter { get; set; }

        public bool HasSetter { get; set; }

        public AccessModifier GetterAccessModifier { get; set; }

        public AccessModifier SetterAccessModifier { get; set; }
    }
}
