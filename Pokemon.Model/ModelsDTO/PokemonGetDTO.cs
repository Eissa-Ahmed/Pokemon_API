using Pokemon.Model.Models;

namespace Pokemon.Model.ModelsDTO
{
    public class PokemonGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public DateTime BirthDate { get; set; }
        public IEnumerable<Review> Review { get; set; }

    }
}
