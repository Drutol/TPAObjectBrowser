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


        internal static TypeMetadata EmitReference(this Type type, AssemblyMetadata assembly)
        {
            if (assembly.RegisteredTypes.Any(metadata => metadata.GetHashCode().Equals(type.GetHashCode())))
            {
                return new TypeMetadata(type.GetHashCode())
                {
                    TypeName = type.Name,
                    NamespaceName = type.GetNamespace(),
                    TypeReference = true,   
                    RootAssembly = assembly
                };
            }

            return null;
        }
    }
}