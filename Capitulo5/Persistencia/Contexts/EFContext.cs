using Modelo.Cadastros;
using Modelo.Tabelas;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Persistencia.Contexts
{
    public class EFContext : DbContext
    {
        public EFContext() : base("ASP_NET_MVC")
        {
            //temporario ate o capitulo6 do Code Fist Migrations que resolve deletar  a base toda ao alterar um dominio(classe).
            Database.SetInitializer<EFContext>(new DropCreateDatabaseIfModelChanges<EFContext>());
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Produto> Produtos { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();// Remove pluralização das tabelas por DEFAULT
        }
    }
}