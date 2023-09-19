namespace Pokemon.Model.IRpo
{
    public interface IUnitOfWork
    {
        public IPokemonRepo pokemon { get; }
        public ICategoryRepo category { get; }
        public ICountryRepo country { get; }
        public IOwnerRepo owner { get; }
        public IReviewRepo review { get; }
        public IReviewerRepo reviewer { get; }
        Task SaveChanges();
    }
}
