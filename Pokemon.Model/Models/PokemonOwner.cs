using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Model.Models
{
    public class PokemonOwner
    {
        [ForeignKey("Pokemon")]
        public int pokemonsId { get; set; }
        [ForeignKey("Owner")]
        public int OwnerId { get; set; }
        public Pokemons pokemons { get; set; }
        public Owner Owner { get; set; }
    }
}
