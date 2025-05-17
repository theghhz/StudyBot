using System.Linq.Expressions;
using StudyBot.Domain.Erros;

namespace StudyBot.Infrastructure.Repositorios;

public interface IBasicCrudRepositorio<T>
{
    Task<int> SaveChanges();
    Task Salvar(T entity);
    Task SalvarLista(List<T> lista);
    Task Atualizar(T entity);
    Task AtualizarLista(List<T> recrutadores);

    Task Remover(T entity);

    Task<T?> BuscarPorId(Guid id);

    Task<Resultado<T>> BuscarPorId(Guid id, ErroEntidade erro);

    Task<List<T>> BuscarTodos();
    Task<List<T>> BuscarPaginado(int skip, int pageSize);

    Task<Resultado<int>> RemoverPorId(Guid id, ErroEntidade erro);

    Task<Resultado<int>> Remover(Expression<Func<T, bool>> condicao, ErroEntidade erro);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    IQueryable<T> FindAll(bool trackChanges = false);
}