using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;

namespace Pokemon.DAL.Repo
{
    public class ReviewerRepo : Repo<Reviewer> , IReviewerRepo
    {
        private readonly ApplicationDbContext dbContext;

        public ReviewerRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Update(Reviewer model)
        {
            var item = await dbContext.Reviewers.FindAsync(model.Id);
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
        }
    }
}
