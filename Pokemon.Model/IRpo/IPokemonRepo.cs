using Pokemon.Model.Models;

namespace Pokemon.Model.IRpo
{
    public interface IPokemonRepo 
    {
        public IEnumerable<Pokemons> GetAll();
        public Pokemons Get(int id);
        public Pokemons Get(string name);
        double GetRate(int id);
        void DeletePokemon(int id);
        bool PokemonExists(int id);
        bool CreatePokemon(IEnumerable<int> categoryId, int OwnerId, Pokemons model);
        void UpdatePokemon(int id, Pokemons model);
    }
}
