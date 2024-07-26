using BhavyaModhiya_490_Model.Context;
using BhavyaModhiya_490_Model.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Helper.ModelConverter
{
    public class UserConverter
    {
        public static ProfileModel ConvertUserToUserModel(Users user)
        {
            ProfileModel userModel = new ProfileModel()
            {
                FirstName = user.FirstName,
                LastName = user.Lastname,
                MiddleName = user.MiddleName,
                Address = user.Address,
                BirthDate = user.BirthDate,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                UserID = user.UserID,                
                ProfilePicture = user.ProfilePhoto,
                ZipCode = user.City.Zipcode,
                Country = user.Country.Name,
                State = user.State.Name,
                City = user.City.Name
            };

            return userModel;
        }
    }
}
