using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiMata.CPPTools.RazorTextGenerator.Models
{
    public class Method
    {
        public string Name { get; set; }

        public NameValueCollection Parameters { get; set; }

        public string ReturnTypeName { get; set; }

        public AccessModifier AccessModifier { get; set; }
    }
}
