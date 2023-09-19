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

    }
}
