using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QiMata.CPPTools.RazorTextGenerator.Templates
{
    public static class EmbeddedResources
    {
        public static string ClassHeaderTemplate = GetEmbeddedString("QiMata.CPPTools.RazorTextGenerator.Templates.ClassHeaderTemplate.cshtml");

        private static string GetEmbeddedString(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }

            throw new ArgumentException(String.Format("Embedded resource {0} not found",resourceName));
        }
    }
}
