using Microsoft.Extensions.DependencyInjection;
using StudyBot.Infrastructure.Db;
using StudyBot.Infrastructure.Repositorios.ConteudoRepo;
using StudyBot.Infrastructure.Repositorios.CronogramaRepo;
using StudyBot.Infrastructure.Repositorios.DiasSemanaRepo;

namespace StudyBot.Infrastructure
{
    public static class IoC
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {   
            services.AddScoped<IConteudoRepositorio, ConteudoRepositorio>();
            services.AddScoped<ICronogramaRepositorio, CronogramaRepositorio>();
            services.AddScoped<IDiasSemanaRepositorio, DiasSemanaRepositorio>();
            
            services.AddDbContext<AppDbContext>();
            return services;
        }
    }
}