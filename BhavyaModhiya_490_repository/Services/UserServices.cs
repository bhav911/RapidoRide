using BhavyaModhiya_490_Model.Context;
using BhavyaModhiya_490_Model.CustomModel;
using BhavyaModhiya_490_repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_repository.Services
{
    public class UserServices : IUserInterface
    {
        private readonly BhavyaModhiya_490Entities _db = new BhavyaModhiya_490Entities();

        public bool RegisterUser(UserModel userModel)
        {
            try
            {
                Users user = new Users()
                {
                    FirstName = userModel.FirstName,
                    MiddleName = userModel.MiddleName,
                    Lastname = userModel.LastName,
                    Address = userModel.AddressLine1 + userModel.AddressLine2,
                    BirthDate = userModel.BirthDate,
                    CityID = userModel.CityID,
                    StateID = userModel.StateID,
                    CountryID = userModel.CountryID,
                    Email = userModel.Email,
                    PhoneNumber = userModel.PhoneNumber,
                    Password = userModel.Password,
                    ProfilePhoto = userModel.ProfilePicture
                };

                Users newUser = _db.Users.Add(user);
                _db.SaveChanges();

                FAVORITES favs = new FAVORITES()
                {
                    BikeIDs = ",",
                    UserID = newUser.UserID
                };
                _db.FAVORITES.Add(favs);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public Users GetProfile(int userID)
        {
            try
            {
                Users user = _db.Users.FirstOrDefault(q => q.UserID == userID);
                return user;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        public bool UpdateProfile(UserModel userModel)
        {
            try
            {
                Users user = _db.Users.FirstOrDefault(q => q.UserID == userModel.UserID);
                if (userModel.ZipCode != _db.City.FirstOrDefault(q => q.CityID == userModel.CityID).Zipcode)
                    return false;
                user.FirstName = userModel.FirstName;
                user.MiddleName = userModel.MiddleName;
                user.Lastname = userModel.LastName;
                user.Address = userModel.AddressLine1 + userModel.AddressLine2;
                user.BirthDate = userModel.BirthDate;
                user.CityID = userModel.CityID;
                user.StateID = userModel.StateID;
                user.CountryID = userModel.CountryID;
                user.PhoneNumber = userModel.PhoneNumber;
                if (userModel.ProfilePicture != null)
                    user.ProfilePhoto = userModel.ProfilePicture;

                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
                throw;
            }
        }

        public Users AuthenticateUser(LoginModel credentials)
        {
            try
            {
                Users user = _db.Users.FirstOrDefault(q => q.Email == credentials.Email && q.Password == credentials.Password);
                return user;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }
    }
}
