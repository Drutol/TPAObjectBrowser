using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.Shared.Extensions
{
    public static class ExtensionMethods
    {
        internal static bool GetVisible(this Type type)
        {
            return type.IsPublic || type.IsNestedPublic || type.IsNestedFamily || type.IsNestedFamANDAssem;
        }

        internal static bool GetVisible(this MethodBase method)
        {
            return method != null && (method.IsPublic || method.IsFamily || method.IsFamilyAndAssembly);
        }

        internal static string GetNamespace(this Type type)
        {
            var ns = type.Namespace;
            return ns ?? string.Empty;
        }


        internal static TypeMetadata EmitReference(this Type type)
        {
            if (!type.IsGenericType)
                return new TypeMetadata
                {
                    TypeName = type.Name,
                    NamespaceName = type.GetNamespace(),
                };
            return new TypeMetadata
            {
                TypeName = type.Name,
                NamespaceName = type.GetNamespace(),
                GenericArguments = EmitGenericArguments(type.GetGenericArguments())
            };
        }

        private static List<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return arguments.Select(EmitReference).ToList();
        }
    }
}