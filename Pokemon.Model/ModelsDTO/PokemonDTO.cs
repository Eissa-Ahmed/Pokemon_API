using Microsoft.AspNetCore.Http;

namespace Pokemon.Model.ModelsDTO
{
    public class PokemonDTO
    {
        public string Name { get; set; }
        public IFormFile? file { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
