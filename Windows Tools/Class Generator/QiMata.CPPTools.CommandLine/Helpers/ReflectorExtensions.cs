using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiMata.CPPTools.CommandLine.Helpers
{
    static class ReflectorExtensions
    {
        public static StringBuilder GetTypeString(this StringBuilder stringBuilder, Type type)
        {
            if (type.IsGenericType)
            {
                stringBuilder.Append(GetGenericTypeName(type)).Append("<");
                foreach (var genericArgument in type.GetGenericArguments())
                {
                    stringBuilder.GetTypeString(genericArgument);
                    if (genericArgument != type.GetGenericArguments().First())
                    {
                        stringBuilder.Append(",");
                    }
                }
                stringBuilder.Append(">");
            }
            else
            {
                stringBuilder.Append(type.FullName ?? type.Name ?? "T");
            }
            return stringBuilder;
        }

        private static string GetGenericTypeName(Type type)
        {
            string typeName = String.Empty;
            var indexOfTilda = type.Name.IndexOf("`");
            var indexOfPlus = type.Name.IndexOf("+");
            if (indexOfTilda != -1)
            {
                typeName = type.Name.Remove(indexOfTilda);
            }
            if (indexOfPlus != -1)
            {
                typeName = type.Name.Remove(0, indexOfPlus);
            }
            if (typeName != string.Empty)
            {
                return typeName;
            }
            return type.FullName;
        }

        public static string GetTypeString(this Type type)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.GetTypeString(type);
            return stringBuilder.ToString();
        }
    }
}
