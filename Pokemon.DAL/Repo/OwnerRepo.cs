using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;

namespace Pokemon.DAL.Repo
{
    public class OwnerRepo : IOwnerRepo
    {
        #region Ctro
        private readonly ApplicationDbContext dbContext;
        public OwnerRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion
        public Owner Get(int id)
        {
            var item = dbContext.Owners.FirstOrDefault(o => o.Id == id);
            return item;
        }

        public IEnumerable<Owner> GetAll()
        {
            var items = dbContext.Owners.OrderBy(i => i.Name).ToList();
            return items;
        }

        public IEnumerable<Owner> GetOwnerByPokemon(int id)
        {
            var items = dbContext.PokemonOwner.Where(i => i.pokemonId == id).Select(i => i.Owner);
            return items;
        }

        public IEnumerable<Pokemons> GetPokemonsByOwner(int id)
        {
            var items = dbContext.PokemonOwner.Where(i => i.OwnerId == id).Select(i => i.Pokemon);
            return items;
        }

        public bool OwnerExist(int id)
        {
            return dbContext.Owners.Any(i => i.Id == id);
        }
    }
}
