using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using StudyBot.Domain;
using StudyBot.Domain.Erros;
using StudyBot.Infrastructure.Db;

namespace StudyBot.Infrastructure.Repositorios.Geral
{

    public abstract class Repositorio<T> where T : Entity
    {
        internal readonly AppDbContext _context;

        internal Repositorio(AppDbContext db)
        {
            _context = db;
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task Salvar(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SalvarLista(List<T> lista)
        {
            await _context.Set<T>().AddRangeAsync(lista);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarLista(List<T> entity)
        {
            foreach (T entityItem in entity)
            {
                _context.Set<T>().Update(entityItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Remover(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<T?> BuscarPorId(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<Resultado<T>> BuscarPorId(Guid id, ErroEntidade erro)
        {
            return await _context.Set<T>().FindAsync(id) is T entity ? entity : (List<ErroEntidade>) [erro];
        }

        public virtual async Task<List<T>> BuscarTodos()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> BuscarPaginado(int skip, int pageSize)
        {
            return await _context.Set<T>().Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<Resultado<int>> RemoverPorId(Guid id, ErroEntidade erro)
        {
            return await Remover(e => e.Id == id, erro);
        }

        public async Task<Resultado<int>> Remover(Expression<Func<T, bool>> condicao, ErroEntidade erro)
        {
            try
            {
                return await _context.Set<T>().Where(condicao).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return (List<ErroEntidade>) [erro];
            }
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
        {
            return !trackChanges
                ? _context.Set<T>().Where(expression).AsNoTracking()
                : _context.Set<T>().Where(expression);
        }

        public IQueryable<T> FindAll(bool trackChanges = false)
        {
            return !trackChanges
                ? _context.Set<T>().AsNoTracking()
                : _context.Set<T>();
        }
    }

}