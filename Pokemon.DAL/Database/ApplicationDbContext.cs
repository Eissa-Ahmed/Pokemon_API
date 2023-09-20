using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pokemon.Model.Models;

namespace Pokemon.DAL.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PokemonOwner>().HasKey(k => new { k.pokemonsId , k.OwnerId});
            modelBuilder.Entity<PokemonCategory>().HasKey(k => new { k.pokemonsId , k.CategoryId});
        }

        public DbSet<Pokemons> pokemons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PokemonCategory> PokemonCategory { get; set; }
        public DbSet<PokemonOwner> PokemonOwner { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

    }
}
