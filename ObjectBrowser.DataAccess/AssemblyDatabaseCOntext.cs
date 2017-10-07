using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.DataAccess
{
    public class AssemblyDatabaseContext : DbContext
    {
        public DbSet<AssemblyMetadata> Assemblies { get; set; }
        public DbSet<EnumFieldMetadata> EnumFieldMetadatas { get; set; }
        public DbSet<FieldMetadata> FieldMetadatas { get; set; }
        public DbSet<MethodMetadata> MethodMetadatas { get; set; }
        public DbSet<NamespaceMetadata> NamespaceMetadatas { get; set; }
        public DbSet<ParameterMetadata> ParameterMetadatas { get; set; }
        public DbSet<PropertyMetadata> PropertyMetadatas { get; set; }
        public DbSet<TypeMetadata> TypeMetadatas { get; set; }
        public DbSet<TypeModifiers> TypeModifiers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=assembly.db"/*, builder => builder.MigrationsAssembly("")*/);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var modelType in GetClassesFromNamespace())
            {
                modelType.GetMethod("OnModelCreating").Invoke(null, new object[] { modelBuilder });
            }

            base.OnModelCreating(modelBuilder);
        }

        private IEnumerable<Type> GetClassesFromNamespace()
        {
            var @interface = typeof(IModelWithRelation);
            string @namespace = "ObjectBrowser.Models.Entities";

            return Assembly.GetAssembly(typeof(AssemblyMetadata))
                .GetTypes()
                .Where(t => t.IsClass && t.Namespace == @namespace && @interface.IsAssignableFrom(t));
        }
    }
}
