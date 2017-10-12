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
            if (assembly.RestrictToNamespace != null)
            {
                if (!type.Namespace.StartsWith(assembly.RestrictToNamespace))
                {
                    return new TypeMetadata(type.GetHashCode())
                    {
                        TypeName = type.Name,
                        NamespaceName = type.GetNamespace(),
                        EndOfTree = true,
                        RootAssembly = assembly
                    };
                }
            }

            if (assembly.RegisteredTypes.Any(metadata => metadata.TypeHash.Equals(type.GetHashCode())))
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

        internal static List<KeyValuePair<string, string>> GetDetails(this TypeMetadata data)
        {
            return new List<KeyValuePair<string, string>>
            {
              new KeyValuePair<string, string>("Access:",data.Modifiers.AccessLevel.ToString()),
              new KeyValuePair<string, string>("IsSealed:",data.Modifiers.IsSealed.ToString()),
              new KeyValuePair<string, string>("IsAbstract:",data.Modifiers.IsAbstract.ToString()),
              new KeyValuePair<string, string>("Namespace:",data.NamespaceName),
              new KeyValuePair<string, string>("Attributes:",string.Join(",",data.Attributes.Select(attribute => attribute.TypeName))),
              new KeyValuePair<string, string>("BaseType:",data.BaseType.TypeName),
              new KeyValuePair<string, string>("Implements:",string.Join(",",data.ImplementedInterfaces.Select(attribute => attribute.TypeName))),
            };
        }

        internal static List<KeyValuePair<string, string>> GetDetails(this MethodMetadata data)
        {
            return new List<KeyValuePair<string, string>>
            {
              new KeyValuePair<string, string>("Access:",data.Modifiers.AccessLevel.ToString()),
              new KeyValuePair<string, string>("IsVirtual:",data.Modifiers.IsVirtual.ToString()),
              new KeyValuePair<string, string>("IsAbstract:",data.Modifiers.IsAbstract.ToString()),
              new KeyValuePair<string, string>("IsStatic:",data.Modifiers.IsStatic.ToString()),
              new KeyValuePair<string, string>("IsExtension:",data.Extension.ToString()),
              new KeyValuePair<string, string>("Params:",string.Join(",",data.Parameters.Select(metadata => $"{metadata.TypeMetadata.TypeName} {metadata.Name}"))),
            };
        }
    }
}