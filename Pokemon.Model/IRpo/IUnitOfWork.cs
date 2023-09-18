namespace Pokemon.Model.IRpo
{
    public interface IUnitOfWork
    {
        public IPokemonRepo pokemon { get; }
        public IReviewerRepo reviewer { get; }
        public IReviewRepo review { get; }
        public ICategoryRepo category { get; }
        Task SaveChanges();
    }
}
