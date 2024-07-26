using BhavyaModhiya_490_Helper.ModelConverter;
using BhavyaModhiya_490_Model.Context;
using BhavyaModhiya_490_Model.CustomModel;
using BhavyaModhiya_490_repository.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BhavyaModhiya_490_WebAPI.Controllers
{
    public class GeographyAPIController : ApiController
    {
        private readonly GeographyServices _geo = new GeographyServices();

        [HttpGet]
        [Route("api/GeographyAPI/GetCountries")]
        public List<CountryModel> GetCountries()
        {
            List<Country> countryList = _geo.GetCountries();
            if (countryList == null)
                return null;
            List<CountryModel> countryModelList = GeoConverter.ConvertCountryToCountryModel(countryList);
            return countryModelList;
        }

        [HttpGet]
        [Route("api/GeographyAPI/GetStates")]
        public List<StateModel> GetStates(int countryID)
        {
            List<State> stateList = _geo.GetStates(countryID);
            if (stateList == null)
                return null;
            List<StateModel> stateModelList = GeoConverter.ConvertStateToStateModel(stateList);
            return stateModelList;
        }

        [HttpGet]
        [Route("api/GeographyAPI/GetCities")]
        public List<CityModel> GetCities(int stateID)
        {
            List<City> cityList = _geo.GetCities(stateID);
            if (cityList == null)
                return null;
            List<CityModel> cityModelList = GeoConverter.ConvertCityToCityModel(cityList);
            return cityModelList;
        }

        [HttpGet]
        [Route("api/GeographyAPI/VerifyZipCode")]
        public GeoModel VerifyZipCode(string zipCode)
        {
            City city = _geo.FetchZip(zipCode);
            if (city == null)
                return null;
            GeoModel geoModel = GeoConverter.ConvertToGeoModel(city);
            geoModel.CountryList = GetCountries();
            geoModel.StateList = GetStates(geoModel.Cid);
            geoModel.CityList = GetCities(geoModel.Sid);
            return geoModel;
        }
    }
}