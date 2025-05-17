
using Microsoft.Extensions.DependencyInjection;
using StudyBot.Application.Services.ConteudoService;
using StudyBot.Application.Services.CronogramaService;

namespace StudyBot.Application
{
    public static class IoC
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<ICronogramaService, CronogramaService>();
            services.AddScoped<IConteudoService, ConteudoService>();
            //services.AddScoped<IDiasSemanaService, DiasSemanaService>();

            return services;
        }
    }
}
