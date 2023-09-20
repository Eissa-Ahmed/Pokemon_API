using Microsoft.AspNetCore.Http;

namespace Pokemon.Model.ModelsDTO
{
    public class PokemonCreateDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public IFormFile file { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
