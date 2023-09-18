using Pokemon.Model.Models;

namespace Pokemon.Model.IRpo
{
    public interface IReviewerRepo : IRepo<Reviewer>
    {
        Task Update(Reviewer model);
    }
}
