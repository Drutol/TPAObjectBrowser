using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ObjectBrowser.Models.Entities;
using ObjectBrowser.Models.Enums;
using ObjectBrowser.Shared.Extensions;

namespace ObjectBrowser.Shared.BL
{
    public class TypeMetadataExtractor : ITypeMetadataExtractor
    {
        private readonly IMethodMetadataExtractor _methodMetadataExtractor;

        public TypeMetadataExtractor(IMethodMetadataExtractor methodMetadataExtractor)
        {
            _methodMetadataExtractor = methodMetadataExtractor;
        }

        public TypeMetadata Extract(Type type)
        {
            return new TypeMetadata
            {
                TypeName = type.Name,
                DeclaringType = EmitDeclaringType(type.DeclaringType),
                Constructors = type.GetConstructors().Select(info => _methodMetadataExtractor.Extract(info)).ToList(),
                Methods = type.GetMethods().Select(info => _methodMetadataExtractor.Extract(info)).ToList(),
                NestedTypes = EmitNestedTypes(type.GetNestedTypes()),
                ImplementedInterfaces = EmitImplements(type.GetInterfaces()),
                GenericArguments =
                    !type.IsGenericTypeDefinition ? null : EmitGenericArguments(type.GetGenericArguments()),
                Modifiers = EmitModifiers(type),
                BaseType = EmitExtends(type),
                Properties = EmitProperties(type.GetProperties()).ToList(),
                TypeKind = GetTypeKind(type),
                Attributes = type.GetCustomAttributes(false).Cast<Attribute>().ToList(),
            };
        }

        public IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return props.Where(prop => prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible())
                .Select(prop => new PropertyMetadata(prop.Name, prop.PropertyType.EmitReference()));
        }

        private List<TypeMetadata> EmitGenericArguments(ICollection<Type> arguments)
        {
            return arguments.Select(type => type.EmitReference()).ToList();
        }

        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            return declaringType.EmitReference();
        }

        private List<TypeMetadata> EmitNestedTypes(ICollection<Type> nestedTypes)
        {
            return nestedTypes.Where(type => type.GetVisible()).Select(Extract).ToList();
        }

        private List<TypeMetadata> EmitImplements(ICollection<Type> interfaces)
        {
            return interfaces.Select(type => type.EmitReference()).ToList();
        }

        private TypeKind GetTypeKind(Type type)
        {
            return type.IsEnum
                ? TypeKind.EnumType
                : type.IsValueType
                    ? TypeKind.StructType
                    : type.IsInterface
                        ? TypeKind.InterfaceType
                        : TypeKind.ClassType;
        }

        private TypeModifiers EmitModifiers(Type type)
        {
            var access = AccessLevel.Private;
            if (type.IsPublic)
                access = AccessLevel.Public;
            else if (type.IsNestedPublic)
                access = AccessLevel.Public;
            else if (type.IsNestedFamily)
                access = AccessLevel.Protected;
            else if (type.IsNestedFamANDAssem)
                access = AccessLevel.ProtectedInternal;

            return new TypeModifiers
            {
                AccessLevel = access,
                IsAbstract = type.IsAbstract,
                IsSealed = type.IsSealed,
            };
        }

        private TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(object) || baseType == typeof(ValueType) ||
                baseType == typeof(Enum))
                return null;
            return baseType.EmitReference();
        }
    }
}
