using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Autofac;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Shared.ViewModels.ItemViewModels;

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

        internal static List<KeyValuePair<string, string>> ToKeyValuePairList(this IEnumerable<(string,string)> tuples)
        {
            return tuples.Select(t => new KeyValuePair<string, string>(t.Item1, t.Item2)).ToList();
        }

        internal static T ResolveWithParameter<T, TParam>(this ILifetimeScope scope, TParam parameter)
        {
            return scope.Resolve<T>(new TypedParameter(typeof(TParam), parameter));
        }
    }
}