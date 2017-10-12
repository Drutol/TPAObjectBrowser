using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObjectBrowser.Interfaces;
using ObjectBrowser.Models.Entities;

namespace ObjectBrowser.DataAccess
{
    [Export(typeof(IDataStorage))]
    public class DatabaseDataStorage : IDataStorage
    {
        public DatabaseDataStorage()
        {
            SQLitePCL.Batteries.Init();

            using (var db = new AssemblyDatabaseContext())
            {
                db.Database.Migrate();
            }
        }

        public async Task Save(AssemblyMetadata assembly)
        {
            using (var db = new AssemblyDatabaseContext())
            {
                db.Database.EnsureDeleted();
                await db.SaveChangesAsync();
                db.Database.Migrate();
                db.Assemblies.Add(assembly);
                await db.SaveChangesAsync();
            }
        }

        public async Task<AssemblyMetadata> Retrieve()
        {
            using (var db = new AssemblyDatabaseContext())
            {
                var asm = await db.Assemblies
                    .AsNoTracking()
                    .Include(metadata => metadata.RegisteredTypes)
                    //type Registers
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Attributes)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Constructors)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.EnumFields)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Fields)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.NestedTypes)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Modifiers)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.BaseType)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.DeclaringType)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.GenericArguments)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.ImplementedInterfaces)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Namespace)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.GenericArguments)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.RootAssembly)
                    //Methods
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.Modifiers)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.RootAssembly)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.Parameters)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.ReturnType)
                    //Constructors
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Constructors)
                    .ThenInclude(metadata => metadata.Modifiers)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Constructors)
                    .ThenInclude(metadata => metadata.RootAssembly)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Constructors)
                    .ThenInclude(metadata => metadata.Parameters)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Constructors)
                    .ThenInclude(metadata => metadata.ReturnType)
                    //Methods - Parameters
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.Parameters)
                    .ThenInclude(metadata => metadata.TypeMetadata)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.Parameters)
                    .ThenInclude(metadata => metadata.MethodMetadata)
                    //Props
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Properties)
                    .ThenInclude(metadata => metadata.RootAssembly)
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Properties)
                    .ThenInclude(metadata => metadata.TypeMetadata)
                    //Fields
                    .Include(metadata => metadata.RegisteredTypes)
                    .ThenInclude(metadata => metadata.Fields)
                    .ThenInclude(metadata => metadata.TypeMetadata)          
                    //type Inner
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Attributes)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Constructors)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.EnumFields)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Fields)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.NestedTypes)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Modifiers)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.BaseType)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.DeclaringType)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.GenericArguments)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.ImplementedInterfaces)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Namespace)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.GenericArguments)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.RootAssembly)
                    //Methods
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.Modifiers)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.RootAssembly)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.Parameters)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.ReturnType)            
                    //Constructors
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Constructors)
                    .ThenInclude(metadata => metadata.Modifiers)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Constructors)
                    .ThenInclude(metadata => metadata.RootAssembly)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Constructors)
                    .ThenInclude(metadata => metadata.Parameters)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Constructors)
                    .ThenInclude(metadata => metadata.ReturnType)   
                    //Methods - Parameters
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.Parameters)
                    .ThenInclude(metadata => metadata.TypeMetadata)   
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Methods)
                    .ThenInclude(metadata => metadata.Parameters)
                    .ThenInclude(metadata => metadata.MethodMetadata)
                    //Props
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Properties)
                    .ThenInclude(metadata => metadata.RootAssembly)
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Properties)
                    .ThenInclude(metadata => metadata.TypeMetadata)
                    //Fields
                    .Include(metadata => metadata.Namespaces)
                    .ThenInclude(metadata => metadata.Types)
                    .ThenInclude(metadata => metadata.Fields)
                    .ThenInclude(metadata => metadata.TypeMetadata)     
                    .FirstAsync();

                //asm.RegisteredTypes = asm.RegisteredTypes.Where(metadata => !metadata.TypeReference && !metadata.EndOfTree).ToList();

                return asm;
            }
        }
    }
}
