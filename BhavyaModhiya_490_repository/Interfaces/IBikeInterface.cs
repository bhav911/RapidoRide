using BhavyaModhiya_490_Model.Context;
using BhavyaModhiya_490_Model.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_repository.Interfaces
{
    public interface IBikeInterface
    {
        List<BikeModel> GetAllBikes(FilterModel filter);
        Bike GetBike(int bikeID);
        bool AddBike(RegisterModel registerModel);
        Seller AddSeller(RegisterModel registerModel);
        bool UpdateRegistery(RegisterModel registerModel);
        bool DeleteBike(int bikeID);
        string GetMyFavorites(int userID);
        bool ToggleFavorit(int userID, int bikeID);
        List<Bike> GetBikesOfIDs(string[] bikeIDs);

    }
}
