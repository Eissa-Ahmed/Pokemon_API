using Pokemon.Model.Models;

namespace Pokemon.Model.IRpo
{
    public interface ICountryRepo
    {
        IEnumerable<Country> GetAll();
        Country Get(int id);
        Country GetCountryByOwner(int id);
        IEnumerable<Owner> GetOwnersFromCountery(int id);
        bool CountryExist(int id);
        void CreateCountry(Country model);
        void UpdateCountry(int id ,Country model);
        void DeleteCountry(int id);

    }
}
