using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;

namespace Pokemon.DAL.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        public IPokemonRepo pokemon { get; private set; }

        public ICategoryRepo category { get; private set; }
        public ICountryRepo country { get; private set; }
        public IOwnerRepo owner { get; private set; }

        public IReviewRepo review { get; private set; }
        public IReviewerRepo reviewer { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext )
        {
            this.dbContext = dbContext;
            pokemon = new PokemonRepo(dbContext);
            category = new CategoryRepo(dbContext);
            country = new CountryRepo(dbContext);
            owner = new OwnerRepo(dbContext);
            review = new ReviewRepo(dbContext);
            reviewer = new ReviewerRepo(dbContext);
        }


        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
