using BhavyaModhiya_490_Model.Context;
using BhavyaModhiya_490_Model.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Helper.ModelConverter
{
    public class GeoConverter
    {
        public static List<CountryModel> ConvertCountryToCountryModel(List<Country> countryList)
        {
            List<CountryModel> countryModelList = new List<CountryModel>();
            foreach(Country country in countryList)
            {
                CountryModel countryModel = new CountryModel()
                {
                    CountryID = country.CountryID,
                    Name = country.Name
                };

                countryModelList.Add(countryModel);
            }

            return countryModelList;
        }

        public static List<StateModel> ConvertStateToStateModel(List<State> stateList)
        {
            List<StateModel> countryModelList = new List<StateModel>();
            foreach (State country in stateList)
            {
                StateModel countryModel = new StateModel()
                {
                    StateID = country.StateID,
                    Name = country.Name
                };

                countryModelList.Add(countryModel);
            }

            return countryModelList;
        }

        public static List<CityModel> ConvertCityToCityModel(List<City> cityList)
        {
            List<CityModel> countryModelList = new List<CityModel>();
            foreach (City country in cityList)
            {
                CityModel countryModel = new CityModel()
                {
                    CityID = country.CityID,
                    Name = country.Name,
                    ZipCode = country.Zipcode
                };

                countryModelList.Add(countryModel);
            }

            return countryModelList;
        }

        public static GeoModel ConvertToGeoModel(City city)
        {
            GeoModel geoModel = new GeoModel()
            {
                Cid = (int)city.State.CountryId,
                Sid = (int)city.StateID,
                Ccid = city.CityID
            };

            return geoModel;
        }
    }
}
