using Pokemon.Model.Models;

namespace Pokemon.Model.IRpo
{
    public interface IPokemonRepo : IRepo<Pokemons>
    {
        public Task Update(Pokemons model);
        public Task<double> GetRate(int id);
    }
}
