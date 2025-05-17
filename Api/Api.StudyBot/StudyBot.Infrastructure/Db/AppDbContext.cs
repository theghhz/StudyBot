using Microsoft.EntityFrameworkCore;
using StudyBot.Domain.Conteudos;
using StudyBot.Domain.Cronogramas;
using StudyBot.Domain.DiasSemanas;
using StudyBot.Infrastructure.Db.ModelConfiguration;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Logging;

namespace StudyBot.Infrastructure.Db;

public class AppDbContext : DbContext
{
    public DbSet<Conteudo> Conteudos { get; set; }
    public DbSet<Cronograma> Cronogramas { get; set; }
    public DbSet<DiasSemana> DiasSemanas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {   
        new ConteudoConfiguration().Configure(modelBuilder.Entity<Conteudo>());
        new CronogramaConfiguration().Configure(modelBuilder.Entity<Cronograma>());
        new DiasSemanaConfiguration().Configure(modelBuilder.Entity<DiasSemana>());
        base.OnModelCreating(modelBuilder); 
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../StudyBot.Api"))
            .AddJsonFile("appsettings.json")
            .Build();
        
        var connectionString = configuration.GetConnectionString("StudyBotApi");

        optionsBuilder
            .UseLazyLoadingProxies()
            .UseNpgsql(connectionString)
            .EnableSensitiveDataLogging()
            .UseLoggerFactory(LoggerFactoryInstance)
            .LogTo(Console.WriteLine, LogLevel.Information);
    }
    
    public static readonly ILoggerFactory LoggerFactoryInstance = LoggerFactory.Create(builder =>
    {
        builder
            .AddConsole()
            .AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information);
    });
}