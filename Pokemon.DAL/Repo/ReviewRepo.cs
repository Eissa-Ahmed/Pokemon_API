using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;

namespace Pokemon.DAL.Repo
{
    public class ReviewRepo : Repo<Review>, IReviewRepo
    {
        private readonly ApplicationDbContext dbContext;

        public ReviewRepo(ApplicationDbContext dbContext) : base(dbContext) 
        {
            this.dbContext = dbContext;
        }
        public async Task Update(Review model)
        {
            var item = await dbContext.Reviews.FindAsync(model.Id);
            item.Title = model.Title;
            item.Description = model.Description;
            item.Rate = model.Rate;
        }
    }
}
