using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;

namespace Pokemon.DAL.Repo
{
    public class ReviewerRepo : IReviewerRepo
    {
        #region Ctor
        private readonly ApplicationDbContext dbContext;
        public ReviewerRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion
        public Reviewer Get(int id)
        {
            var item = dbContext.Reviewers.FirstOrDefault(x => x.Id == id);
            return item;
        }

        public IEnumerable<Reviewer> GetAll()
        {
            var items = dbContext.Reviewers.ToList();
            return items;
        }

        public IEnumerable<Review> GetReviewsByReviewer(int id)
        {
            var items = dbContext.Reviews.Where(i => i.ReviewerId == id).ToList();
            return items;
        }

        public bool ReviewerExist(int id)
        {
            return dbContext.Reviewers.Any(i => i.Id == id);
        }

        public void CreateReviewer(Reviewer model)
        {
            dbContext.Reviewers.Add(model);
        }
        public void DeleteReviewer(int id)
        {
            var item = dbContext.Reviewers.Find(id);
            dbContext.Reviewers.Remove(item);
        }
        public void UpdateReviewer(int id, Reviewer model)
        {
            var item = dbContext.Reviewers.Find(id);
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;

        }
    }
}
