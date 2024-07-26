using BhavyaModhiya_490_Model.Context;
using BhavyaModhiya_490_Model.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_repository.Interfaces
{
    public interface IUserInterface
    {
        bool RegisterUser(UserModel user);
        Users AuthenticateUser(LoginModel credentials);
        Users GetProfile(int userID);
        bool UpdateProfile(UserModel userModel);
    }
}
