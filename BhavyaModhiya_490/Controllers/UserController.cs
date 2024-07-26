using BhavyaModhiya_490.CustomFilters;
using BhavyaModhiya_490.SessionHelper;
using BhavyaModhiya_490.WebAPI;
using BhavyaModhiya_490_Model.CustomModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BhavyaModhiya_490.Controllers
{
 
    [CustomAuthorizeHelperAttribute]
    [CustomCustomerAuthenticateHelper]

    public class UserController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public async Task<PartialViewResult> GetFilteredBikes(FilterModel filter)
        {
            string response = await WebApiHelper.HttpClientRequestResponsePost("api/BikeAPI/GetFilteredBikes", JsonConvert.SerializeObject(filter));
            List<BikeModel> bikeModelList = JsonConvert.DeserializeObject<List<BikeModel>>(response);
            response = await WebApiHelper.HttpClientRequestResponseGet("api/BikeAPI/GetMyFavorites?userID=" + UserSession.UserID);
            string[] bikeIDs = JsonConvert.DeserializeObject<string>(response).Split(',');
            foreach (BikeModel bike in bikeModelList)
            {
                if (bikeIDs.Contains(Convert.ToString(bike.BikeID)))
                {
                    bike.IsFavorite = true;
                }
            }
            return PartialView(bikeModelList);
        }

        public ActionResult Unauthorize()
        {
            return View();
        }

        public async Task<ActionResult> GetBikeDetails(int bikeID)
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/BikeAPI/GetBikeDetails?bikeID=" + bikeID);
            BikeModel bikeModel = JsonConvert.DeserializeObject<BikeModel>(response);
            response = await WebApiHelper.HttpClientRequestResponseGet("api/BikeAPI/GetMyFavorites?userID=" + UserSession.UserID);
            string[] bikeIDs = JsonConvert.DeserializeObject<string>(response).Split(',');
            if (bikeIDs.Contains(Convert.ToString(bikeModel.BikeID)))
            {
                bikeModel.IsFavorite = true;
            }
            return View(bikeModel);
        }

        public async Task<JsonResult> ToggleFavorites(int bikeID)
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet($"api/BikeAPI/ToggleFavorites?bikeID={bikeID}&userID={UserSession.UserID}");
            bool status = JsonConvert.DeserializeObject<bool>(response);
            if (!status)
            {
                TempData["error"] = "Something Went Wrong";
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetMyFavorites()
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/BikeAPI/GetMyFavorites?userID=" + UserSession.UserID);
            string[] bikeIDs = JsonConvert.DeserializeObject<string>(response).Split(',');
            response = await WebApiHelper.HttpClientRequestResponsePost("api/BikeAPI/GetBikesOfIDs", JsonConvert.SerializeObject(bikeIDs));
            List<BikeModel> bikeModelList = JsonConvert.DeserializeObject<List<BikeModel>>(response);
            return View(bikeModelList);
        }

        public async Task<ActionResult> GetProfile()
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/UserAPI/GetProfile?userID=" + UserSession.UserID);
            ProfileModel userModel = JsonConvert.DeserializeObject<ProfileModel>(response);
            if (userModel == null)
            {
                TempData["error"] = "Something Went Wrong";
                return RedirectToAction("Home");
            }
            return View(userModel);
        }

        public async Task<ActionResult> UpdateProfile()
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/UserAPI/GetProfile?userID=" + UserSession.UserID);
            ProfileModel userModel = JsonConvert.DeserializeObject<ProfileModel>(response);
            if(userModel == null)
            {
                TempData["error"] = "Something Went Wrong";
                return RedirectToAction("Home");
            }
            response = await WebApiHelper.HttpClientRequestResponseGet("api/GeographyAPI/VerifyZipCode?zipCode=" + userModel.ZipCode);
            userModel.geoModel = JsonConvert.DeserializeObject<GeoModel>(response);
            return View(userModel);
        }
        
        [HttpPost]
        public async Task<ActionResult> UpdateProfile(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                if (userModel.ProfilePictureFile != null)
                {
                    const int maxSizeInBytes = 3 * 1024 * 1024;

                    if (userModel.ProfilePictureFile.ContentLength > maxSizeInBytes)
                    {
                        ModelState.AddModelError("ProfilePictureFile", "File size must be less than 3 MB.");
                        return View(userModel);
                    }
                    userModel.ProfilePicture = GetUniqueFileName(userModel.ProfilePictureFile);
                }
                userModel.ProfilePictureFile = null;

                string response = await WebApiHelper.HttpClientRequestResponsePost("api/UserAPI/UpdateProfie", JsonConvert.SerializeObject(userModel));
                bool status = JsonConvert.DeserializeObject<bool>(response);
                if (!status)
                {
                    TempData["error"] = "Please Enter Valid Zipcode";
                    return RedirectToAction("UpdateProfile");
                }
                TempData["success"] = "Profile Updated Successfully";
                return RedirectToAction("GetProfile");
            }
            TempData["error"] = "Invalid Data";
            return RedirectToAction("UpdateProfile");
        }

        [NonAction]
        private string GetUniqueFileName(HttpPostedFileBase file)
        {
            if (file == null)
                return null;
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            file.SaveAs(HttpContext.Server.MapPath("~/Content/UserProfile/") + uniqueFileName);
            return uniqueFileName;
        }
    }
}