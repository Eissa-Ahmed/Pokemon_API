﻿using Pokemon.Model.Models;

namespace Pokemon.Model.IRpo
{
    public interface IOwnerRepo
    {
        IEnumerable<Owner> GetAll();
        Owner Get(int id);
        IEnumerable<Owner> GetOwnerByPokemon(int id);
        IEnumerable<Pokemons> GetPokemonsByOwner(int id);
        bool OwnerExist(int id);
        void CreateOwner(Owner model);
        void UpdateOwner(int id ,Owner model);
        void DeleteOwner(int id);
    }
}
