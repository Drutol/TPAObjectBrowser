using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Enums;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class MethodMetadata : IModelWithRelation
    {
        public long Id { get; set; }
        //vars
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public MethodModifiers Modifiers { get; set; }
        [DataMember]
        public AssemblyMetadata RootAssembly { get; set; }
        [DataMember]
        public TypeMetadata ReturnType { get; set; }
        [DataMember]
        public bool Extension { get; set; }
        [DataMember]
        public virtual List<TypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public virtual List<ParameterMetadata> Parameters { get; set; }

        [IgnoreDataMember]
        public TypeMetadata ParentTypeA { get; set; }
        [IgnoreDataMember]
        public TypeMetadata ParentTypeB { get; set; }
        [IgnoreDataMember]
        public TypeMetadata ParentTypeC { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MethodMetadata>()
                .HasMany(a => a.GenericArguments)
                .WithOne(m => m.ParentMethod);

            modelBuilder.Entity<MethodMetadata>()
                .HasMany(a => a.Parameters)
                .WithOne(r => r.MethodMetadata);

            modelBuilder.Entity<MethodMetadata>()
                .HasOne(t => t.Modifiers)
                .WithOne(modifiers => modifiers.ParentMethod);
        }
    }
}