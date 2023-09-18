using AutoMapper;
using Pokemon.Model.Models;
using Pokemon.Model.ModelsDTO;

namespace Pokemon.DAL.Helper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Pokemons, PokemonDTO>().ReverseMap();
            CreateMap<Pokemons, PokemonGetDTO>().ReverseMap();
            CreateMap<Reviewer, ReviewerDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
