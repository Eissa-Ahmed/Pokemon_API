using AutoMapper;
using Pokemon.Model.Models;
using Pokemon.Model.ModelsDTO;

namespace Pokemon.DAL.Helper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Pokemons , PokemonDTO>().ReverseMap();
            CreateMap<Category , CategoryDTO>().ReverseMap();
            CreateMap<Review , ReviewDTO>().ReverseMap();
            CreateMap<Reviewer , ReviewerDTO>().ReverseMap();
            CreateMap<Country , CountryDTO>().ReverseMap();
            CreateMap<Owner , OwnerDTO>().ReverseMap();

        }
    }
}
