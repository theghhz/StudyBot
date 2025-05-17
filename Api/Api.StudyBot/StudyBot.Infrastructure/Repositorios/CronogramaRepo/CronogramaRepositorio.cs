using Microsoft.EntityFrameworkCore;
using StudyBot.Domain.Cronogramas;
using StudyBot.Infrastructure.Db;
using StudyBot.Infrastructure.Repositorios.Geral;

namespace StudyBot.Infrastructure.Repositorios.CronogramaRepo
{
   internal class CronogramaRepositorio(AppDbContext dbContext) : Repositorio<Cronograma>(dbContext) , ICronogramaRepositorio
   {
      public override Task<Cronograma?> BuscarPorId(Guid id)
      {
         return dbContext.Cronogramas
            .Include(c => c.Dias)
            .ThenInclude(d => d.Conteudos)
            .FirstOrDefaultAsync(c => c.Id == id);
      }
   }
}