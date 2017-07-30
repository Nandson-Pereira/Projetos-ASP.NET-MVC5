using Capitulo1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Capitulo1.Contexts
{
    public class EFContext : DbContext
    {
        public EFContext() : base ("ASP_NET_MVC")
        {  
            //temporario ate o capitulo6 do Code Fist Migrations que resolve deletar  a base toda ao alterar um dominio(classe).
            Database.SetInitializer<EFContext>( new DropCreateDatabaseIfModelChanges<EFContext>());
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

    }
}