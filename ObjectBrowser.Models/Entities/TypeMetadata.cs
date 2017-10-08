using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class TypeMetadata : IModelWithRelation
    {
        public long Id { get; set; }

        public TypeMetadata()
        {

        }

        public TypeMetadata(int typeHash)
        {
            TypeHash = typeHash;
        }
        [DataMember]
        public string TypeName { get;  set; }
        [DataMember]
        public string NamespaceName { get;  set; }
        [DataMember]
        public AssemblyMetadata RootAssembly { get; set; }
        [DataMember]
        public TypeMetadata BaseType { get;  set; }
        [DataMember]
        public TypeModifiers Modifiers{ get;  set; }
        [DataMember]
        public TypeMetadata DeclaringType { get;  set; }
        [DataMember]
        public TypeKind TypeKind{ get;  set; }

        [DataMember]
        public virtual List<TypeMetadata> Attributes{ get;  set; }
        [DataMember]
        public virtual List<TypeMetadata> ImplementedInterfaces{ get;  set; }
        [DataMember]
        public virtual List<TypeMetadata> NestedTypes{ get;  set; }
        [DataMember]
        public virtual List<PropertyMetadata> Properties{ get;  set; }
        [DataMember]
        public virtual List<TypeMetadata> GenericArguments { get;  set; }
        [DataMember]
        public virtual List<MethodMetadata> Methods{ get;  set; }
        [DataMember]
        public virtual List<MethodMetadata> Constructors{ get; set; }
        [DataMember]
        public virtual List<EnumFieldMetadata> EnumFields{ get; set; }
        [DataMember]
        public virtual List<FieldMetadata> Fields{ get; set; }

        [DataMember]
        public int TypeHash { get; set; }
        [DataMember]
        public bool TypeReference { get; set; }
        [DataMember]
        public bool EndOfTree { get; set; }

        [IgnoreDataMember]
        public NamespaceMetadata Namespace { get; set; }

        [IgnoreDataMember]
        public TypeMetadata ParentTypeA { get; set; }
        [IgnoreDataMember]
        public TypeMetadata ParentTypeB { get; set; }
        [IgnoreDataMember]
        public TypeMetadata ParentTypeC { get; set; }
        [IgnoreDataMember]
        public TypeMetadata ParentTypeD { get; set; }
        [IgnoreDataMember]
        public TypeMetadata ParentTypeE { get; set; }
        [IgnoreDataMember]
        public TypeMetadata ParentTypeF { get; set; }

        [IgnoreDataMember]
        public MethodMetadata ParentMethod { get; set; }

        public override int GetHashCode()
        {
            return TypeHash;
        }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypeMetadata>()
                .HasMany(a => a.Attributes)
                .WithOne(n => n.ParentTypeA);

            modelBuilder.Entity<TypeMetadata>()
                .HasMany(a => a.ImplementedInterfaces)
                .WithOne(r => r.ParentTypeB);

            modelBuilder.Entity<TypeMetadata>()
                .HasMany(a => a.NestedTypes)
                .WithOne(r => r.ParentTypeC);

            modelBuilder.Entity<TypeMetadata>()
                .HasMany(a => a.Properties)
                .WithOne(r => r.ParentType);

            modelBuilder.Entity<TypeMetadata>()
                .HasMany(a => a.GenericArguments)
                .WithOne(r => r.ParentTypeE);

            modelBuilder.Entity<TypeMetadata>()
                .HasMany(a => a.Methods)
                .WithOne(r => r.ParentTypeA);

            modelBuilder.Entity<TypeMetadata>()
                .HasMany(a => a.Constructors)
                .WithOne(r => r.ParentTypeB);

            modelBuilder.Entity<TypeMetadata>()
                .HasMany(a => a.EnumFields)
                .WithOne(r => r.ParentType);

            modelBuilder.Entity<TypeMetadata>()
                .HasMany(a => a.Fields)
                .WithOne(r => r.ParentType);

            modelBuilder.Entity<TypeMetadata>()
                .HasOne(t => t.Modifiers)
                .WithOne(modifiers => modifiers.ParentType);

            modelBuilder.Entity<TypeMetadata>()
                .HasOne(t => t.DeclaringType)
                .WithOne(modifiers => modifiers.ParentTypeD);

            modelBuilder.Entity<TypeMetadata>()
                .HasOne(t => t.BaseType)
                .WithOne(modifiers => modifiers.ParentTypeF);
        }
    }
}