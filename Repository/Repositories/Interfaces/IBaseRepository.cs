using Domain.Common;
using Domain.Entities;
using System.Linq.Expressions;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task EditAsync(T entity);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllWithExpression(Expression<Func<T,bool>> predicate);
        Task<T> GetWithExpression(Expression<Func<T,bool>> predicate);
        Task DeleteAsync(int id);




    }
}
