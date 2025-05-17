using System.Linq.Expressions;
using StudyBot.Domain.Conteudo;
using StudyBot.Domain.Conteudos;
using StudyBot.Domain.Erros;
using StudyBot.Infrastructure.Db;
using StudyBot.Infrastructure.Repositorios;
using StudyBot.Infrastructure.Repositorios.Geral;

namespace StudyBot.Infrastructure.Repositorios.ConteudoRepo
{
    internal class ConteudoRepositorio(AppDbContext dbContext) : Repositorio<Conteudo>(dbContext), IConteudoRepositorio
    {
    }
}