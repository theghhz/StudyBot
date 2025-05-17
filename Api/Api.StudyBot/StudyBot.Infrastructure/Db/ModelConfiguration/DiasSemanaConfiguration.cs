using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudyBot.Domain.DiasSemanas;

namespace StudyBot.Infrastructure.Db.ModelConfiguration;

public class DiasSemanaConfiguration : IEntityTypeConfiguration<DiasSemana>
{
    public void Configure(EntityTypeBuilder<DiasSemana> builder)
    {
        builder.ToTable(nameof(DiasSemana));

        builder.HasKey(d => d.Id);

        builder.Property(d => d.DiaEnum)
            .IsRequired();

        builder.Property(d => d.Data)
            .IsRequired();

        builder.HasMany(d => d.Conteudos)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}