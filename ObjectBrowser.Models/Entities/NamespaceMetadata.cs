using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using ObjectBrowser.Interfaces;

namespace ObjectBrowser.Models.Entities
{
    [DataContract(IsReference = true)]
    public class NamespaceMetadata : IModelWithRelation
    {
        public long Id { get; set; }

        public NamespaceMetadata()
        {
            
        }

        public NamespaceMetadata(string namespaceName, ICollection<TypeMetadata> types)
        {
            NamespaceName = namespaceName;
            Types = types.ToList();
        }

        [DataMember]
        public AssemblyMetadata RootAssembly { get; set; }
        [DataMember]
        public string NamespaceName { get; set; }
        [DataMember]
        public virtual List<TypeMetadata> Types { get; set; }

        public static void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NamespaceMetadata>()
                .HasMany(a => a.Types)
                .WithOne(metadata => metadata.Namespace);

        }
    }
}