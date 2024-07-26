using BhavyaModhiya_490_Model.Context;
using BhavyaModhiya_490_Model.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_repository
{
    public interface IGeographyInterface
    {
        List<Country> GetCountries();
        List<State> GetStates(int countryID);
        List<City> GetCities(int stateID);
    }
}
