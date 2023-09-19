using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;

namespace Pokemon.DAL.Repo
{
    public class ReviewRepo : IReviewRepo
    {
        #region Ctor
        private readonly ApplicationDbContext dbContext;
        public ReviewRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        public IEnumerable<Review> GetAll()
        {
            var items = dbContext.Reviews.ToList();
            return items;
        }

        public Review GetById(int id)
        {
            var item = dbContext.Reviews.FirstOrDefault(i => i.Id == id);
            return item;
        }

        public IEnumerable<Review> GetReviewByPokemon(int id)
        {
            var data = dbContext.Reviews.Where(i => i.PokemonId == id);
            return data;
        }

        public bool ReviewExist(int id)
        {
            return dbContext.Reviews.Any(i => i.Id == id);
        }
    }
}
