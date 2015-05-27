using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NDesk.Options;
using QiMata.CPPTools.CommandLine.Helpers;
using QiMata.CPPTools.RazorTextGenerator;
using QiMata.CPPTools.RazorTextGenerator.Models;

namespace QiMata.CPPTools.CommandLine
{
    class Program
    {
        private static string _assemblyPath;
        private static string _outputPath;

        static void Main(string[] args)
        {
            var options = new OptionSet()
            {
                { "assembly=", (string filePath) =>
                {
                    _assemblyPath = filePath;
                }},
                {"outputPath=", (string outputFolder)=> { _outputPath = outputFolder; } }
            };
            var extras = options.Parse(args);
            Reflector reflector = new Reflector(_assemblyPath);
            var types = reflector.GeCppTypeModels().ToList();
            var classGenerator = new ClassGenerator();

            if (!Directory.Exists(_outputPath))
            {
                Directory.CreateDirectory(_outputPath);
                foreach (string file in Directory.GetFiles(_outputPath))
                {
                    File.Delete(file);
                }
            }

            foreach (CPPTypeModel cppTypeModel in types)
            {
                bool illegal = false;
                var illegalCharacters = Path.GetInvalidFileNameChars();
                for (int i = 0; i < cppTypeModel.ClassName.Length; i++)
                {
                    for (int j = 0; j < illegalCharacters.Length; j++)
                    {
                        if (illegalCharacters[j] == cppTypeModel.ClassName[i])
                        {
                            illegal = true;
                        }
                    }
                }
                if (!illegal)
                {
                    using (var stream = File.CreateText(_outputPath + "\\\\" + cppTypeModel.ClassName + ".hpp"))
                    {
                        stream.Write(classGenerator.GenerateClass(cppTypeModel));
                    }
                }
            }
        }
    }
}
