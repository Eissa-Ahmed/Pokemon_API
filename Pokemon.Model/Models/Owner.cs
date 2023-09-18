using System.ComponentModel.DataAnnotations.Schema;

namespace Pokemon.Model.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gym { get; set; }
        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public IEnumerable<PokemonOwner> PokemonOwners { get; set; }
    }
}
