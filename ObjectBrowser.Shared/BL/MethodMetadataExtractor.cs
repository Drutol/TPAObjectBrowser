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
        public MethodMetadata Extract(MethodBase method)
        {
            return new MethodMetadata
            {
                Name = method.Name,
                GenericArguments = !method.IsGenericMethodDefinition
                    ? null
                    : method.GetGenericArguments().Select(type => type.EmitReference()).ToList(),
                ReturnType = EmitReturnType(method),
                Parameters = EmitParameters(method.GetParameters()),
                Modifiers = EmitModifiers(method),
                Extension = EmitExtension(method),
            };
        }

        private List<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return parms.Select(
                parm => new ParameterMetadata(parm.Name, parm.ParameterType.EmitReference())).ToList();
        }

        private TypeMetadata EmitReturnType(MethodBase method)
        {
            var methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return methodInfo.ReturnType.EmitReference();
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
