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
    public class UserAPIController : ApiController
    {
        private readonly UserServices _user = new UserServices();

        [HttpPost]
        [Route("api/UserAPI/RegisterUser")]
        public bool RegisterUser(UserModel userModel)
        {
            bool status = _user.RegisterUser(userModel);
            return status;
        }

        [HttpPost]
        [Route("api/UserAPI/UpdateProfie")]
        public bool UpdateProfie(UserModel userModel)
        {
            bool status = _user.UpdateProfile(userModel);
            return status;
        }

        [HttpPost]
        [Route("api/UserAPI/AuthenticateUser")]
        public ProfileModel AuthenticateUser(LoginModel credentials)
        {
            Users user = _user.AuthenticateUser(credentials);
            if (user == null)
                return null;
            ProfileModel userModel = UserConverter.ConvertUserToUserModel(user);
            return userModel;
        }

        [HttpGet]
        [Route("api/UserAPI/GetProfile")]
        public ProfileModel GetProfile(int userID)
        {
            Users user = _user.GetProfile(userID);
            if (user == null)
                return null;
            ProfileModel userModel = UserConverter.ConvertUserToUserModel(user);
            return userModel;
        }
    }
}