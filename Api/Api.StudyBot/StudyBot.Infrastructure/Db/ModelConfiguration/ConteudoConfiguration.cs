using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyBot.Domain.Conteudos;

namespace StudyBot.Infrastructure.Db.ModelConfiguration;

public class ConteudoConfiguration : IEntityTypeConfiguration<Conteudo>
{   
    public void Configure(EntityTypeBuilder<Conteudo> builder)
    {
        builder.ToTable(nameof(Conteudo));
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasMaxLength(500);
        
    }
}