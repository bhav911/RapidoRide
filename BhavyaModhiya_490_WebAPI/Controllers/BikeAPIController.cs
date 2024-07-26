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
    public class BikeAPIController : ApiController
    {
        private readonly BikeService _bike = new BikeService();

        [HttpGet]
        [Route("api/BikeAPI/GetAllBikes")]
        public List<BikeModel> GetAllBikes()
        {
            List<Bike> bikeList = _bike.GetAllBikes();
            if (bikeList == null)
                return null;
            List<BikeModel> bikeModelList = BikeConverter.ConvertBikeListToBikeModelList(bikeList);
            return bikeModelList;
        }

        [HttpPost]
        [Route("api/BikeAPI/GetFilteredBikes")]
        public List<BikeModel> GetFilteredBikes(FilterModel filter)
        {
            List<BikeModel> bikeList = _bike.GetAllBikes(filter);
            return bikeList;
        }

        [HttpGet]
        [Route("api/BikeAPI/GetBike")]
        public BikeModel GetBike(int bikeID)
        {
            Bike bike = _bike.GetBike(bikeID);
            if (bike == null)
                return null;
            BikeModel bikeModel = BikeConverter.ConvertBikeToBikeModel(bike);
            return bikeModel;
        }

        [HttpPost]
        [Route("api/BikeAPI/AddBike")]
        public bool AddBike(RegisterModel registerModel)
        {
            try
            {
                Seller seller = _bike.AddSeller(registerModel);
                registerModel.SellerID = seller.SellerID;
                bool status = _bike.AddBike(registerModel);
                return status;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        [HttpPost]
        [Route("api/BikeAPI/UpdateBike")]
        public bool UpdateBike(RegisterModel registerModel)
        {
            bool status = _bike.UpdateRegistery(registerModel);
            return status;
        }

        [HttpGet]
        [Route("api/BikeAPI/DeleteBike")]
        public bool DeleteBike(int bikeID)
        {
            bool status = _bike.DeleteBike(bikeID);
            return status;
        }

        [HttpGet]
        [Route("api/BikeAPI/GetBikeDetails")]
        public BikeModel GetBikeDetails(int bikeID)
        {
            Bike bike = _bike.GetBike(bikeID);
            if (bike == null)
                return null;
            BikeModel bikeModel = BikeConverter.ConvertBikeToBikeModel(bike);
            return bikeModel;
        }

        [HttpGet]
        [Route("api/BikeAPI/GetMyFavorites")]
        public string GetMyFavorites(int userID)
        {
            string bikeIDs = _bike.GetMyFavorites(userID);
            return bikeIDs;
        }

        [HttpGet]
        [Route("api/BikeAPI/ToggleFavorites")]
        public bool ToggleFavorites(int userID, int bikeID)
        {
            bool status = _bike.ToggleFavorit(userID, bikeID);
            return status;
        }

        [HttpPost]
        [Route("api/BikeAPI/GetBikesOfIDs")]
        public List<BikeModel> GetBikesOfIDs(string[] bikeIDs)
        {
            List<Bike> bikeList = _bike.GetBikesOfIDs(bikeIDs);
            if (bikeList == null)
                return null;
            List<BikeModel> bikeModelList = BikeConverter.ConvertBikeListToBikeModelList(bikeList);
            return bikeModelList;
        }


    }
}