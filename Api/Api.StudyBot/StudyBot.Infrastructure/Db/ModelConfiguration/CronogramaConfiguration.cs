using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyBot.Domain.Cronogramas;

namespace StudyBot.Infrastructure.Db.ModelConfiguration;

public class CronogramaConfiguration : IEntityTypeConfiguration<Cronograma>
{
    public void Configure(EntityTypeBuilder<Cronograma> builder)
    {
        builder.ToTable(nameof(Cronograma));
        
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasMany(c => c.Dias)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}