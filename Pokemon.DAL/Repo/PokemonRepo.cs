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

        #region Ctrate Pokemon
        public bool CreatePokemon(int categoryId, int OwnerId, Pokemons model)
        {
            var owner = dbContext.Owners.Where(i => i.Id == OwnerId).FirstOrDefault();
            var category = dbContext.Categories.Where(i => i.Id == categoryId).FirstOrDefault();

            var PokemonOwner = new PokemonOwner()
            {
                pokemons = model,
                Owner = owner,
            };
            dbContext.PokemonOwner.Add(PokemonOwner);

            var PokemonCategory = new PokemonCategory()
            {
                pokemons = model,
                Category = category,
            };
            dbContext.PokemonCategory.Add(PokemonCategory);

            dbContext.pokemons.Add(model);
            return true;
        }
        #endregion

        #region Delete
        public void DeletePokemon(int id)
        {
            var pokemonCategory = dbContext.PokemonCategory.Where(i => i.pokemonsId == id).ToList();
            dbContext.PokemonCategory.RemoveRange(pokemonCategory);
            var pokemonOwner = dbContext.PokemonOwner.Where(i => i.pokemonsId == id).ToList();
            dbContext.PokemonOwner.RemoveRange(pokemonOwner);
            var item = dbContext.pokemons.Find(id);
            dbContext.pokemons.Remove(item);
        }
        #endregion

        #region Update Pokemon
        public void UpdatePokemon(int id, Pokemons model)
        {
            var item = dbContext.pokemons.Find(id);
            item.Name = model.Name;
            item.BirthDate = model.BirthDate;

            if (model.ImageUrl != null)
            {
                item.ImageUrl = model.ImageUrl;
            }
        }
        #endregion

        #endregion


    }
}
