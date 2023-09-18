using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Model.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(1,5)]
        public double Rate { get; set; }
        [ForeignKey("ReviewerId")]
        public int ReviewerId { get; set;}
        [ForeignKey("Pokemon")]
        public int PokemonId { get; set;}
        public Reviewer Reviewer { get; set; } 
        public Pokemons Pokemon { get; set; }
    }
}
