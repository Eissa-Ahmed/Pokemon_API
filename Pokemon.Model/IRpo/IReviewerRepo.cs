using Pokemon.Model.Models;

namespace Pokemon.Model.IRpo
{
    public interface IReviewerRepo
    {
        IEnumerable<Reviewer> GetAll();
        IEnumerable<Review> GetReviewsByReviewer(int id);
        Reviewer Get(int id);
        bool ReviewerExist(int id);
    }
}
