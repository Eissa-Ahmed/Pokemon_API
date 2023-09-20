using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;

namespace Pokemon.DAL.Repo
{
    public class CategoryRepo : ICategoryRepo
    {
        #region Ctro
        private readonly ApplicationDbContext dbContext;
        public CategoryRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion


        #region Methods

        #region GetPokemonsByCategory
        public IEnumerable<Pokemons> GetPokemonsByCategory(int id) // id Category
        {
            var items = dbContext.PokemonCategory.Where(i => i.CategoryId == id).Select( i => i.pokemons).ToList();
            return items;
        }
        #endregion

        #region GetById
        public Category Get(int id)
        {
            var item = dbContext.Categories.FirstOrDefault(c => c.Id == id);
            return item;
        }

        #endregion

        #region GetAll
        public IEnumerable<Category> GetAll()
        {
            var items = dbContext.Categories.OrderBy(p => p.Id).ToList();
            return items;
        }
        #endregion

        #region CategoryExist
        public bool CategoryExists(int id)
        {
            return dbContext.Categories.Any(i => i.Id == id);
        }


        #endregion

        #region Create Category
        public bool CreateCategory(Category model)
        {
            dbContext.Categories.Add(model);
            return true;
        }


        #endregion

        #region Delete Category
        public void DeleteCategory(int id)
        {
            var item = dbContext.Categories.FirstOrDefault(i => i.Id == id);
            dbContext.Categories.Remove(item);
        }
        #endregion

        #region Update Category
        public void UpdateCategory(int id ,Category model)
        {
            var item = dbContext.Categories.Find(id);
            item.Name = model.Name;
        }
        #endregion


        #endregion


    }
}
