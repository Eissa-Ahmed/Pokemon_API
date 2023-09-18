using Pokemon.Model.Models;

namespace Pokemon.Model.IRpo
{
    public interface ICategoryRepo : IRepo<Category>
    {
        public void Update(Category model);
    }
}
