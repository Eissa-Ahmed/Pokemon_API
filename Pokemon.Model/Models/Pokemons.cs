namespace Pokemon.Model.Models
{
    public class Pokemons
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<Review> Review { get; set; }
        public IEnumerable<PokemonOwner> PokemonOwner { get; set; }
        public IEnumerable<PokemonCategory>? PokemonCategory { get; set; }

    }
}
