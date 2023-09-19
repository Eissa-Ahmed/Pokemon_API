using Pokemon.Model.Models;

namespace Pokemon.Model.IRpo
{
    public interface ICategoryRepo
    {
        public IEnumerable<Category> GetAll();
        public Category Get(int id);
        public IEnumerable<Pokemons> GetPokemonsByCategory(int id);
        bool CategoryExists(int id);
    }
}
