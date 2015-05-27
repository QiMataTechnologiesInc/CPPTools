﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QiMata.CPPTools.RazorTextGenerator.Models;

namespace QiMata.CPPTools.RazorTextGenerator.Templates
{
    public static class TemplateHelpers
    {
        public static string GenerateNamespaceString(CPPTypeModel typeModel)
        {
            var initial = typeModel.Namespaces.Aggregate("__", (current, ns) => current + (ns.ToUpper() + "__"));
            return initial + typeModel.ClassName.ToUpper() + "__HPP__";
        }

        public static string GenerateNamespaceAndClassDeclaration(CPPTypeModel typeModel)
        {
            var tabCount = 0;

            var initial = new StringBuilder();
            foreach (string ns in typeModel.Namespaces)
            {
                initial.AppendTabs(tabCount).Append("namespace ").Append(ns).AppendLine().AppendTabs(tabCount).Append("{").AppendLine();
                tabCount++;
            }

            initial.AppendTabs(tabCount).Append("class ").Append(typeModel.ClassName).AppendLine();
            initial.AppendTabs(tabCount).Append("{").AppendLine();
            initial.AppendTabs(tabCount).Append("\t").Append("public:").AppendLine();
            GeneratePublicProperties(initial,typeModel, tabCount);
            initial.AppendTabs(tabCount).Append("\t").Append("protected:").AppendLine();
            initial.AppendTabs(tabCount).Append("\t").Append("private:").AppendLine();
            initial.AppendTabs(tabCount).Append("};").AppendLine();

            foreach (string ns in typeModel.Namespaces)
            {
                tabCount--;
                initial.AppendTabs(tabCount).Append("};").AppendLine();
            }

            return initial.ToString();
        }

        private static void GeneratePublicProperties(StringBuilder initial, CPPTypeModel typeModel, int tabCount)
        {
            foreach (Property property in typeModel.Properties.Where(x => x.HasGetter && x.GetterAccessModifier == AccessModifier.Public))
            {
                initial.AppendTabs(tabCount)
                    .Append("\t\t").GetPublicPropertyGetter(property)
                    .AppendLine();
            }
        }

        private static StringBuilder GetPublicPropertyGetter(this StringBuilder initial, Property property)
        {
            initial.Append(property.TypeName).Append(" ").Append("get").Append(property.Name).Append("();").AppendLine();
            return initial;
        }

        private static StringBuilder AppendTabs(this StringBuilder stringBuilder,int tabCount)
        {
            for (int i = 0; i < tabCount; i++)
            {
                stringBuilder.Append('\t');
            }
            return stringBuilder;
        }
    }
}
