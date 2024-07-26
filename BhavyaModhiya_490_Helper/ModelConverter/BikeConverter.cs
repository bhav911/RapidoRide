using BhavyaModhiya_490_Model.Context;
using BhavyaModhiya_490_Model.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Helper.ModelConverter
{
    public class BikeConverter
    {
        public static List<BikeModel> ConvertBikeListToBikeModelList(List<Bike> bikeList)
        {
            List<BikeModel> bikeModelList = new List<BikeModel>();
            foreach(Bike bike in bikeList)
            {
                BikeModel bikeModel = new BikeModel()
                {
                    BikeID = bike.BikeID,
                    Brand = bike.Brand,
                    Models = bike.Models,
                    Description = bike.Description,
                    KilometresDriven = bike.kilometresDriven,
                    Price = bike.Price,
                    SellerID = bike.SellerID,
                    Years = bike.Years,
                    FirstName = bike.Seller.FirstName,
                    LastName = bike.Seller.LastName,
                    Email = bike.Seller.Email,
                    PhoneNumber = bike.Seller.PhoneNumber,
                    Image = bike.Image
                };

                bikeModelList.Add(bikeModel);
            }

            return bikeModelList;
        }

        public static BikeModel ConvertBikeToBikeModel(Bike bike)
        {
            BikeModel bikeModel = new BikeModel()
            {
                BikeID = bike.BikeID,
                Brand = bike.Brand,
                Models = bike.Models,
                Description = bike.Description,
                KilometresDriven = bike.kilometresDriven,
                Price = bike.Price,
                SellerID = bike.SellerID,
                Years = bike.Years,
                FirstName = bike.Seller.FirstName,
                LastName = bike.Seller.LastName,
                Email = bike.Seller.Email,
                PhoneNumber = bike.Seller.PhoneNumber,
                Image = bike.Image
            };

            return bikeModel;
        }
    }
}
