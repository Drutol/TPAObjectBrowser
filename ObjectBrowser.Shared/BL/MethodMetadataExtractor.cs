using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Models.Enums;
using ObjectBrowser.Shared.Extensions;

namespace ObjectBrowser.Shared.BL
{
    public class MethodMetadataExtractor : IMethodMetadataExtractor
    {
        public MethodMetadata Extract(MethodBase method, AssemblyMetadata rootAssembly, ITypeMetadataExtractor extractor)
        {
            var methodData = new MethodMetadata
            {
                Name = method.Name,
                GenericArguments = !method.IsGenericMethodDefinition
                    ? null
                    : EmitGenericArguments(method.GetGenericArguments(),rootAssembly,extractor).ToList(),
                ReturnType = EmitReturnType(method, rootAssembly,extractor),
                Parameters = EmitParameters(method.GetParameters(), rootAssembly,extractor).ToList(),
                Modifiers = EmitModifiers(method),
                Extension = EmitExtension(method),
            };

            methodData.RootAssembly = rootAssembly;

            return methodData;
        }

        private IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> param, AssemblyMetadata rootAssembly,ITypeMetadataExtractor extractor)
        {
            foreach (var type in param)
            {
                yield return type.EmitReference(rootAssembly) ?? extractor.Extract(type, rootAssembly); ;
            }
        }

        private IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> param, AssemblyMetadata rootAssembly, ITypeMetadataExtractor extractor)
        {
            foreach (var parameterInfo in param)
            {
                yield return new ParameterMetadata(parameterInfo.Name, parameterInfo.ParameterType.EmitReference(rootAssembly) ?? extractor.Extract(parameterInfo.ParameterType, rootAssembly));
            }          
        }

        private TypeMetadata EmitReturnType(MethodBase method, AssemblyMetadata rootAssembly, ITypeMetadataExtractor extractor)
        {
            var methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;

            return methodInfo.ReturnType.EmitReference(rootAssembly) ?? extractor.Extract(methodInfo.ReturnType, rootAssembly);


        }

        private bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private MethodModifiers EmitModifiers(MethodBase method)
        {
            var access = AccessLevel.Private;
            if (method.IsPublic)
                access = AccessLevel.Public;
            else if (method.IsFamily)
                access = AccessLevel.Protected;
            else if (method.IsFamilyAndAssembly)
                access = AccessLevel.ProtectedInternal;

            return new MethodModifiers
            {
                AccessLevel = access,
                IsAbstract = method.IsAbstract,
                IsStatic = method.IsStatic,
                IsVirtual = method.IsVirtual,
            };
        }
    }
}
