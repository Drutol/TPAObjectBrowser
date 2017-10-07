using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using ObjectBrowser.Interfaces;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class AssemblyMetadata : IModelWithRelation
    {
        public long Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public virtual List<NamespaceMetadata> Namespaces { get; set; }
        [DataMember]
        public virtual List<TypeMetadata> RegisteredTypes { get; set; } = new List<TypeMetadata>();

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssemblyMetadata>()
                .HasMany(a => a.Namespaces)
                .WithOne(n => n.RootAssembly);

            modelBuilder.Entity<AssemblyMetadata>()
                .HasMany(a => a.RegisteredTypes)
                .WithOne(r => r.RootAssembly);
        }
    }
}