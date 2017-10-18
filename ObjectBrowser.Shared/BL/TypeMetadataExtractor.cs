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
    internal class TypeMetadataExtractor : ITypeMetadataExtractor
    {
        private readonly IMethodMetadataExtractor _methodMetadataExtractor;

        public TypeMetadataExtractor(IMethodMetadataExtractor methodMetadataExtractor)
        {
            _methodMetadataExtractor = methodMetadataExtractor;
        }

        public TypeMetadata Extract(Type type, AssemblyMetadata rootAssembly)
        {
            var typeData = new TypeMetadata(type.GetHashCode());

            if(!rootAssembly.RegisteredTypes.Any(metadata => metadata.TypeHash == typeData.GetHashCode()))
                rootAssembly.RegisteredTypes.Add(typeData);

            typeData.TypeName = type.Name;
            typeData.DeclaringType = EmitDeclaringType(type.DeclaringType, rootAssembly);
            typeData.Constructors = type.GetConstructors()
                .Select(info => _methodMetadataExtractor.Extract(info, rootAssembly, this)).ToList();
            typeData.Methods = type.GetMethods().Where(info => !info.IsSpecialName)
                .Select(info => _methodMetadataExtractor.Extract(info, rootAssembly, this))
                .ToList();
            typeData.NestedTypes = EmitNestedTypes(type.GetNestedTypes(), rootAssembly);
            typeData.ImplementedInterfaces = EmitImplements(type.GetInterfaces(), rootAssembly).ToList();
            typeData.GenericArguments =
                !type.IsGenericTypeDefinition
                    ? null
                    : EmitGenericArguments(type.GetGenericArguments(), rootAssembly).ToList();
            typeData.Modifiers = EmitModifiers(type);
            typeData.BaseType = EmitExtends(type, rootAssembly);
            typeData.Properties = EmitProperties(type.GetProperties(), rootAssembly).ToList();
            typeData.TypeKind = GetTypeKind(type);
            typeData.Attributes = EmitAttributes(type, rootAssembly).ToList();
            typeData.EnumFields = EmitEnumFields(type).ToList();
            typeData.Fields = EmitFields(type,rootAssembly).ToList();


            typeData.RootAssembly = rootAssembly;

            return typeData;
        }

        private IEnumerable<TypeMetadata> EmitAttributes(Type type, AssemblyMetadata rootAssembly)
        {
            foreach (var attr in type.GetCustomAttributes(false))
            {
                yield return attr.GetType().EmitReference(rootAssembly) ?? Extract(attr.GetType(),rootAssembly);
            }
        }

        private IEnumerable<FieldMetadata> EmitFields(Type type, AssemblyMetadata rootAssembly)
        {
            foreach (var fieldInfo in type.GetFields())
            {
                if (!fieldInfo.FieldType.IsEnum)
                {
                    yield return new FieldMetadata(fieldInfo.Name,fieldInfo.FieldType.EmitReference(rootAssembly) ?? Extract(fieldInfo.FieldType,rootAssembly));
                }
            }
        }

        private IEnumerable<EnumFieldMetadata> EmitEnumFields(Type type)
        {
            foreach (var fieldInfo in type.GetFields())
            {
                if (fieldInfo.FieldType.IsEnum)
                {
                    int val = 0;
                    try
                    {
                        val = (int) fieldInfo.GetRawConstantValue();
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    yield return new EnumFieldMetadata(fieldInfo.Name,val);
                }
            }
        }

        private IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props,
            AssemblyMetadata rootAssembly)
        {
            foreach (var propertyInfo in props.Where(prop =>
                prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()))
            {
                TypeMetadata typeMetadata;

                typeMetadata = propertyInfo.PropertyType.EmitReference(rootAssembly) ??
                               Extract(propertyInfo.PropertyType, rootAssembly);

                yield return new PropertyMetadata(propertyInfo.Name, typeMetadata);
            }
        }

        private IEnumerable<TypeMetadata> EmitGenericArguments(ICollection<Type> arguments,
            AssemblyMetadata rootAssembly)
        {
            foreach (var type in arguments)
            {
                yield return type.EmitReference(rootAssembly) ?? Extract(type, rootAssembly);
                ;
            }
        }

        private TypeMetadata EmitDeclaringType(Type declaringType, AssemblyMetadata rootAssembly)
        {
            if (declaringType == null)
                return null;

            return declaringType.EmitReference(rootAssembly) ?? Extract(declaringType, rootAssembly);


        }

        private List<TypeMetadata> EmitNestedTypes(ICollection<Type> nestedTypes, AssemblyMetadata rootAssembly)
        {
            return nestedTypes.Where(type => type.GetVisible()).Select(type => Extract(type, rootAssembly)).ToList();
        }

        private IEnumerable<TypeMetadata> EmitImplements(ICollection<Type> interfaces, AssemblyMetadata rootAssembly)
        {
            foreach (var @interface in interfaces)
            {
                yield return @interface.EmitReference(rootAssembly) ?? Extract(@interface, rootAssembly);
                ;
                ;
            }
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

        private TypeMetadata EmitExtends(Type baseType, AssemblyMetadata rootAssembly)
        {
            if (baseType == null || baseType == typeof(object) || baseType == typeof(ValueType) ||
                baseType == typeof(Enum))
                return null;

            return baseType.EmitReference(rootAssembly) ?? Extract(baseType, rootAssembly);

        }
    }
}
