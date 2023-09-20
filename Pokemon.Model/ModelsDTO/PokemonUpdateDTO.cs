using Microsoft.AspNetCore.Http;

namespace Pokemon.Model.ModelsDTO
{
    public class PokemonUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? file { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
