using FluentValidation.Results;
using IMDb.Core;
using IMDb.Core.Interfaces;
using IMDb.Data.Entidades;
using IMDb.Data.Mapeamentos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace IMDb.Data.Contextos
{
    public class IMDbContexto : DbContext, IUnitOfWork
    {
        public IMDbContexto(DbContextOptions<IMDbContexto> options) : base(options) { }

        public DbSet<Voto> Votos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Filme> Filmes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IMDbContexto).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(t => typeof(Entity).IsAssignableFrom(t.ClrType)))
            {
                var entityTypeBuilder = modelBuilder.Entity(entityType.ClrType);
                entityTypeBuilder.Ignore("Guid");
            }

            modelBuilder.ApplyConfiguration(new FilmeMap());
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
