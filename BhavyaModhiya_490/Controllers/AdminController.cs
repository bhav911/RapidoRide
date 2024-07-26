using BhavyaModhiya_490.CustomFilters;
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
    [CustomAdminAuthentucateHelperAttribute]
    public class AdminController : Controller
    {
        public async Task<ActionResult> Home()
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/BikeAPI/GetAllBikes");
            List<BikeModel> bikeModelList = JsonConvert.DeserializeObject<List<BikeModel>>(response);
            return View(bikeModelList);
        }

        public ActionResult AddBike()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddBike(RegisterModel registerModel, List<HttpPostedFileBase> BikeImages)
        {
            if(BikeImages == null || BikeImages.Count() > 5 || BikeImages.Count() < 1)
            {
                ModelState.AddModelError("BikeImages", "Only 5 Images are allowed");
                return View(registerModel);
            }
            foreach (HttpPostedFileBase file in BikeImages)
            {
                const int maxSizeInBytes = 3 * 1024 * 1024;

                if (file.ContentLength > maxSizeInBytes)
                {
                    ModelState.AddModelError("BikeImages", "Image size must be less than 3 MB.");
                    return View(registerModel);
                }
            }

            if (ModelState.IsValid)
            {
                string aggregatedProductImages = "";
                foreach (HttpPostedFileBase file in BikeImages)
                {
                    aggregatedProductImages += GetUniqueFileName(file) + ",";
                }                 
                registerModel.Image = aggregatedProductImages.Substring(0, aggregatedProductImages.Length - 1);
                string response = await WebApiHelper.HttpClientRequestResponsePost("api/BikeAPI/AddBike", JsonConvert.SerializeObject(registerModel));
                bool status = JsonConvert.DeserializeObject<bool>(response);
                if (!status)
                {
                    TempData["error"] = "Failed to add Bike";
                    return View(registerModel);
                }
                TempData["success"] = "Bike added successfully";
                return RedirectToAction("Home");
            }

            return View(registerModel);
        }

        public async Task<ActionResult> EditBike(int bikeID)
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/BikeAPI/GetBike?bikeID="+bikeID);
            BikeModel bikeModel = JsonConvert.DeserializeObject<BikeModel>(response);
            if(bikeModel == null)
            {
                TempData["error"] = "Something Went Wrong";
                return RedirectToAction("Home");
            }
            return View("AddBike", bikeModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditBike(RegisterModel registerModel, List<HttpPostedFileBase> BikeImages)
        {

            if (BikeImages == null || BikeImages.Count() > 5 || BikeImages.Count() < 1)
            {
                ModelState.AddModelError("BikeImages", "Only 5 Images are allowed");
                return View(registerModel);
            }
            foreach (HttpPostedFileBase file in BikeImages)
            {
                const int maxSizeInBytes = 3 * 1024 * 1024;

                if (file.ContentLength > maxSizeInBytes)
                {
                    ModelState.AddModelError("BikeImages", "Image size must be less than 3 MB.");
                    return View(registerModel);
                }
            }

            if (ModelState.IsValid)
            {
                string aggregatedProductImages = "";
                foreach (HttpPostedFileBase file in BikeImages)
                {
                    aggregatedProductImages += GetUniqueFileName(file) + ",";
                }
                registerModel.Image = aggregatedProductImages.Substring(0, aggregatedProductImages.Length - 1);

                string response = await WebApiHelper.HttpClientRequestResponsePost("api/BikeAPI/UpdateBike", JsonConvert.SerializeObject(registerModel));
                bool status = JsonConvert.DeserializeObject<bool>(response);
                if (!status)
                {
                    TempData["error"] = "Failed to edit Bike";
                    return View(registerModel);
                }
                TempData["success"] = "Bike Updated Successfully";
                return RedirectToAction("Home");
            }
            return View(registerModel);
            
        }

        public async Task<ActionResult> DeleteBike(int bikeID)
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/BikeAPI/DeleteBike?bikeID=" + bikeID);
            bool status = JsonConvert.DeserializeObject<bool>(response);
            if (!status)
            {
                TempData["error"] = "Failed to delete Bike";
                return RedirectToAction("Home");
            }
            TempData["success"] = "Bike Deleted Successfully";
            return RedirectToAction("Home");
        }

        [NonAction]
        private string GetUniqueFileName(HttpPostedFileBase file)
        {
            if (file == null)
                return null;
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            file.SaveAs(HttpContext.Server.MapPath("~/Content/BikeImages/") + uniqueFileName);
            return uniqueFileName;
        }

        public async Task<ActionResult> GetBikeDetails(int bikeID)
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/BikeAPI/GetBikeDetails?bikeID=" + bikeID);
            BikeModel bikeModelList = JsonConvert.DeserializeObject<BikeModel>(response);
            if (bikeModelList == null)
            {
                TempData["error"] = "Something Went Wrong";
                return RedirectToAction("Home");
            }
            return View(bikeModelList);
        }

        public ActionResult Unauthorize()
        {
            return View();
        }
    }
}