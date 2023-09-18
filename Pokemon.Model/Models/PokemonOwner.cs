using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Model.Models
{
    public class PokemonOwner
    {
        [ForeignKey("Pokemon")]
        public int pokemonId { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public Pokemons Pokemon { get; set; }
        public Owner Owner { get; set; }
    }
}
