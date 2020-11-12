using IMDb.Data.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDb.Data.Mapeamentos
{
    public class FilmeMap : IEntityTypeConfiguration<Filme>
    {
        public void Configure(EntityTypeBuilder<Filme> builder)
        {
            builder.Property(e => e.Media)
                .HasColumnType("decimal(2, 1)")
                .IsRequired(true);
        }   
    }
}
