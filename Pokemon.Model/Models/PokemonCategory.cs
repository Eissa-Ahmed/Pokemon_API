using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Model.Models
{
    public class PokemonCategory
    {
        [ForeignKey("Pokemon")]
        public int pokemonsId { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Pokemons pokemons { get; set; }
        public Category Category { get; set; }
    }
}
