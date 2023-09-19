using Pokemon.DAL.Database;
using Pokemon.Model.IRpo;
using Pokemon.Model.Models;

namespace Pokemon.DAL.Repo
{
    public class CountryRepo : ICountryRepo
    {
        #region Ctro
        private readonly ApplicationDbContext dbContext;
        public CountryRepo(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion


        #region Methods

        #region Country Exist
        public bool CountryExist(int id)
        {
            return dbContext.Countries.Any(c => c.Id == id);
        }
        #endregion

        #region Get
        public Country Get(int id)
        {
            var item = dbContext.Countries.FirstOrDefault(c => c.Id == id);
            return item;
        }
        #endregion

        #region GetAll
        public IEnumerable<Country> GetAll()
        {
            var items = dbContext.Countries.OrderBy(i => i.Id).ToList();
            return items;
        }
        #endregion

        #region Get Country By Owner
        public Country GetCountryByOwner(int id)
        {
            var country = dbContext.Owners.Where(c => c.Id == id).Select(i => i.Country).FirstOrDefault();
            return country;
        }
        #endregion

        #region Get Owners From Countery
        public IEnumerable<Owner> GetOwnersFromCountery(int id)
        {
            var owners = dbContext.Owners.Where(i => i.CountryId == id).ToList();
            return owners;
        }
        #endregion

        #endregion
    }
}
