using System.Linq.Expressions;

namespace Pokemon.Model.IRpo
{
    public interface IRepo<T> where T : class
    {
        Task<T> Get(Expression<Func<T,bool>> filter, string? include = null);
        Task Create(T model);
        void Delete(T model);
        Task<IEnumerable<T>> GetAllAsync(string? include = null);
    }
}
