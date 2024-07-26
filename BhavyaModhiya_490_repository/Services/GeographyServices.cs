using BhavyaModhiya_490_Model.Context;
using BhavyaModhiya_490_Model.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_repository.Services
{
    public class GeographyServices : IGeographyInterface
    {
        private readonly BhavyaModhiya_490Entities _db = new BhavyaModhiya_490Entities();
        public List<Country> GetCountries()
        {
            try
            {
                List<Country> countryList = _db.Country.ToList();
                return countryList;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        public List<State> GetStates(int countryID)
        {
            try
            {
                List<State> stateList = _db.State.Where(q => q.CountryId == countryID).ToList();
                return stateList;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
        public List<City> GetCities(int stateID)
        {
            try
            {
                List<City> cityList = _db.City.Where(q => q.StateID == stateID).ToList();
                return cityList;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public City FetchZip(string zipCode)
        {
            City city = _db.City.FirstOrDefault(q => q.Zipcode == zipCode);
            return city;
        }
    }
}
