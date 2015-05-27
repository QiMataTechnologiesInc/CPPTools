using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using QiMata.CPPTools.RazorTextGenerator.Models;

namespace QiMata.CPPTools.CommandLine.Helpers
{
    class Reflector
    {
        private readonly Assembly _assembly;

        public Reflector(Assembly assembly)
        {
            _assembly = assembly;
        }

        public Reflector(string assemblyPath)
        {
            _assembly = Assembly.LoadFile(assemblyPath);
        }

        public IEnumerable<CPPTypeModel> GeCppTypeModels()
        {
            var types = _assembly.GetTypes();
            return types.Select(ConvertType).Where(x => x != null);
        }

        private CPPTypeModel ConvertType(Type type)
        {
            if (type.Namespace == null)
            {
                return null;
            }
            if (type.IsAbstract)
            {
                return ConvertAbstractType(type);
            }
            else if (type.IsEnum)
            {
                return ConvertEnumType(type);
            }
            else if (type.IsGenericType)
            {
                return ConvertGenericType(type);
            }
            else if (type.IsInterface)
            {
                return ConvertInterfaceType(type);
            }
            else if(type.IsValueType || type.IsClass)
            {
                return ConvertClassOrStructType(type);
            }
            else
            {
                return null;
            }
        }

        private CPPTypeModel ConvertClassOrStructType(Type type)
        {
            return BasicTypeConversion(type);
        }

        private CPPTypeModel ConvertInterfaceType(Type type)
        {
            //TODO: Update
            return ConvertClassOrStructType(type);
        }

        private CPPTypeModel ConvertGenericType(Type type)
        {
            return BasicTypeConversion(type);
        }

        private CPPTypeModel ConvertEnumType(Type type)
        {
            //TODO: Update
            return ConvertClassOrStructType(type);
        }

        private CPPTypeModel ConvertAbstractType(Type type)
        {
            //TODO: Update
            return ConvertClassOrStructType(type);
        }

        private CPPTypeModel BasicTypeConversion(Type type)
        {
            var model = new CPPTypeModel
            {
                ClassName = type.Name,
                Namespaces = type.Namespace?.Split(new string[] {"."}, StringSplitOptions.RemoveEmptyEntries),
                Properties = type.GetProperties().Select(ConvertProperty),
                DirectInheritanceTypes = GetInheritance(type),
                TemplateParameters = GetTemplateParameters(type),
                Methods = GetMethods(type)
            };
            
            return model;
        }

        private IEnumerable<Method> GetMethods(Type type)
        {
            var methods = type.GetMethods();
            return methods.Select(methodInfo => new Method
            {
                AccessModifier = methodInfo.IsPublic ? AccessModifier.Public : AccessModifier.Private,
                Name = methodInfo.Name,
                ReturnTypeName = methodInfo.ReturnType.FullName,
                Parameters = GetMethodParameters(methodInfo)
            });
        }

        private NameValueCollection GetMethodParameters(MethodInfo methodInfo)
        {
            NameValueCollection nameValueCollection = new NameValueCollection();
            foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
            {
                nameValueCollection.Add(parameterInfo.Name,parameterInfo.ParameterType.FullName);
            }
            return nameValueCollection;
        }

        private IEnumerable<string> GetTemplateParameters(Type type)
        {
            return type.GetGenericArguments().Select(x => x.FullName);
        }

        private IEnumerable<string> GetInheritance(Type type)
        {
            if (type.BaseType != null)
            {
                yield return type.BaseType.FullName;
            }
            if (type.GetInterfaces().Any())
            {
                foreach (Type interfaceType in type.GetInterfaces())
                {
                    yield return interfaceType.FullName;
                }
            }
        }

        private Property ConvertProperty(PropertyInfo arg)
        {
            var prop = new Property
            {
                Name = arg.Name,
                TypeName = arg.PropertyType.FullName,
            };
            var setMethod = arg.GetSetMethod();
            if (arg.CanWrite && setMethod != null)
            {
                prop.HasSetter = true;
                if (setMethod.IsPublic)
                {
                    prop.SetterAccessModifier = AccessModifier.Public;
                }
                else if (setMethod.IsPrivate)
                {
                    prop.SetterAccessModifier = AccessModifier.Private;
                }
            }
            var getMethod = arg.GetGetMethod();
            if (arg.CanRead && getMethod != null)
            {
                prop.HasGetter = true;
                if (getMethod.IsPublic)
                {
                    prop.GetterAccessModifier = AccessModifier.Public;
                }
                if (getMethod.IsPrivate)
                {
                    prop.GetterAccessModifier = AccessModifier.Private;
                }
            }
            return prop;
        }
    }
}
