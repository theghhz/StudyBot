using StudyBot.Domain.DiasSemanas;
using StudyBot.Infrastructure.Db;
using StudyBot.Infrastructure.Repositorios.Geral;

namespace StudyBot.Infrastructure.Repositorios.DiasSemanaRepo
{
    internal class DiasSemanaRepositorio(AppDbContext dbContext) : Repositorio<DiasSemana>(dbContext), IDiasSemanaRepositorio
    {
    }
}