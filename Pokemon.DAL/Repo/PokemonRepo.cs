using Microsoft.EntityFrameworkCore;
using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;

namespace Pokemon.DAL.Repo
{
    public class PokemonRepo : Repo<Pokemons> , IPokemonRepo
    {
        private readonly ApplicationDbContext dbContext;
        public PokemonRepo(ApplicationDbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<double> GetRate(int id)
        {
            var reviews = await dbContext.Reviews.Where(i => i.PokemonId == id).ToListAsync();
            var ratres =  reviews.Sum(s => s.Rate) / reviews.Count();
            if(ratres > 0)
            {
                return ratres;
            }
            else
            {
                return 0;
            }
            
        }

        public async Task Update(Pokemons model)
        {
            var item = await dbContext.pokemons.FindAsync(model.Id);
            if (item == null)
            {
                item.Name = model.Name;
                item.BirthDate = model.BirthDate;
                item.ImageUrl = model.ImageUrl;
            }
        }
    }
}
