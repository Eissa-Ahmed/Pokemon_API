using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;

namespace Pokemon.DAL.Repo
{
    public class PokemonRepo : IPokemonRepo
    {
        #region Ctro
        private readonly ApplicationDbContext dbContext;
        public PokemonRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion


        #region Methods

        #region GetById
        public Pokemons Get(int id)
        {
            var item = dbContext.pokemons.Where(i => i.Id == id).SingleOrDefault();
            return item;
        }

        #endregion

        #region GetByName
        public Pokemons Get(string name)
        {
            var item = dbContext.pokemons.Where(i => i.Name == name).SingleOrDefault();
            return item;
        }
        #endregion

        #region GetAll
        public IEnumerable<Pokemons> GetAll()
        {
            var items = dbContext.pokemons.OrderBy(p => p.Id).ToList();
            return items;
        }
        #endregion

        #region GetRate
        public double GetRate(int id)
        {
            var reviews = dbContext.Reviews.Where(i => i.PokemonId == id).ToList();
            if (reviews.Count() <= 0)
                return 0;

            return reviews.Sum(i => i.Rate) / reviews.Count();
        }
        #endregion

        #region PokemonExist
        public bool PokemonExists(int id)
        {
            return dbContext.pokemons.Any(i => i.Id == id);
        }
        #endregion

        #endregion


    }
}
