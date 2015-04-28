using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiMata.CPPTools.RazorTextGenerator.Models
{
    public class CPPTypeModel
    {
        public IEnumerable<string> Namespaces { get; set; }

        public string ClassName { get; set; }
    }
}
