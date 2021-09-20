using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagazineStore.Core.Entidades;

namespace MagazineStore.Core.Context
{
    public class MagazineStoreContext : DbContext
    {
        public MagazineStoreContext() : base("MagazineStoreConnectionString") { }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Revista> Revistas { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer<MagazineStoreContext>(new DropCreateDatabaseAlways<MagazineStoreContext>());
            //modelBuilder.Conventions.Remove<TableAttributeConvention>();
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Cliente>()
                .HasRequired(c => c.Endereco)
                .WithRequiredPrincipal(e => e.Cliente);

            modelBuilder.Entity<Revista>().HasMany(r => r.Movimentacoes)
                .WithMany(m => m.Revistas)
                .Map(m =>
                         {
                             m.MapLeftKey("sRevistaID");
                             m.MapRightKey("sMovimentacaoID");
                             m.ToTable("tbMovimentacaoRevista");
                         });
        }
    }
}
