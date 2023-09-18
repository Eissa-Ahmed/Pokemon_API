using Microsoft.EntityFrameworkCore;
using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;
using System.Linq.Expressions;

namespace Pokemon.DAL.Repo
{
    public class Repo<T> : IRepo<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;
        public Repo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Create(T model)
        {
            await dbContext.Set<T>().AddAsync(model);
        }

        public void Delete(T model)
        {
            dbContext.Set<T>().Remove(model);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter, string? include = null)
        {
            if (include is null)
            {
                var item = await dbContext.Set<T>().SingleOrDefaultAsync(filter);
                return item;
            }
            else
            {
                var item = await dbContext.Set<T>().Include(include).SingleOrDefaultAsync(filter);
                return item;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(string? include = null)
        {
            if (include is null)
            {
                var items = await dbContext.Set<T>().AsNoTracking().ToListAsync();
                return items;
            }
            else
            {
                var items = await dbContext.Set<T>().Include(include).AsNoTracking().ToListAsync();
                return items;
            }
        }
    }
}
