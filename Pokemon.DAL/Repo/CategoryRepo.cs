using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;

namespace Pokemon.DAL.Repo
{
    public class CategoryRepo : Repo<Category>, ICategoryRepo
    {
        private readonly ApplicationDbContext dbContext;

        public CategoryRepo(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Update(Category model)
        {
            var item = dbContext.Categories.Find(model.Id);
            item.Name = model.Name;
        }
    }
}
