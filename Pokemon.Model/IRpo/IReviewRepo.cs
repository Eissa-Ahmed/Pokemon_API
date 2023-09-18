using Pokemon.Model.Models;

namespace Pokemon.Model.IRpo
{
    public interface IReviewRepo : IRepo<Review>
    {
        public Task Update(Review model);
    }
}
