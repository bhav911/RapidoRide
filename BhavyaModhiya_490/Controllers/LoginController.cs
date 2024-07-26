using BhavyaModhiya_490.SessionHelper;
using BhavyaModhiya_490.WebAPI;
using BhavyaModhiya_490_Model.CustomModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BhavyaModhiya_490.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult SignIn()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignIn(LoginModel credentials, string Role)
        {
            if(Role == null)
            {
                TempData["error"] = "Please Select Role";
                return View(credentials);
            }
            if (Role == "Admin")
            {
                if (credentials.Email == "superadmin@gmail.com" && credentials.Password == "123456")
                {
                    UserSession.Username = "SuperAdmin";
                    UserSession.UserID = 1;
                    UserSession.UserRole = "Admin";
                    TempData["Role"] = "Admin";
                    return RedirectToAction("Home", "Admin");
                }
            }

            if (ModelState.IsValid)
            {                
                if(Role == "User")
                {
                    string response = await WebApiHelper.HttpClientRequestResponsePost("api/UserAPI/AuthenticateUser", JsonConvert.SerializeObject(credentials));
                    ProfileModel status = JsonConvert.DeserializeObject<ProfileModel>(response);
                    if (status == null)
                    {
                        TempData["error"] = "Invalid Credentials";
                        return View(credentials);
                    }

                    UserSession.Username = status.Email;
                    UserSession.UserID = (int)status.UserID;
                    UserSession.UserRole = "User";
                    TempData["Role"] = "User";
                    return RedirectToAction("Home", "User");
                }
            }
            return View(credentials);
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                string password = GenaretePassword();
                userModel.Password = password;
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
                string response = await WebApiHelper.HttpClientRequestResponsePost("api/UserAPI/RegisterUser", JsonConvert.SerializeObject(userModel));
                bool status = JsonConvert.DeserializeObject<bool>(response);
                if(!status)
                {
                    TempData["error"] = "User Already Exist";
                    return View(userModel);
                }
                try
                {
                    SendMail(userModel.Email, password);
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Failed to send mail.";
                    throw;
                }               
                TempData["success"] = "User Registered Successfully";
                return RedirectToAction("SignIn");
            }
            return View(userModel);
        }

        public ActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }

        public ActionResult UnderConstruction()
        {
            return View();
        }

        [NonAction]
        public string GenaretePassword()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [NonAction]
        private void SendMail(string email, string password)
        {
            try
            {
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                using (MailMessage mm = new MailMessage(smtpSection.From, email))
                {
                    mm.Subject = "Registered Successfully! Login Credentials.";
                    mm.Body = $"Your Login password id {password}";
                    mm.IsBodyHtml = false;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = smtpSection.Network.Host;
                        smtp.EnableSsl = smtpSection.Network.EnableSsl;
                        NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        smtp.UseDefaultCredentials = smtpSection.Network.DefaultCredentials;
                        smtp.Credentials = networkCred;
                        smtp.Port = smtpSection.Network.Port;
                        smtp.Send(mm);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JsonResult> GetCountries()
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/GeographyAPI/GetCountries");
            List<CountryModel> CountryModelList = JsonConvert.DeserializeObject<List<CountryModel>>(response);
            return Json(CountryModelList, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetStates(int countryID)
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/GeographyAPI/GetStates?countryID=" + countryID);
            List<StateModel> StateModelList = JsonConvert.DeserializeObject<List<StateModel>>(response);
            return Json(StateModelList, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetCities(int stateID)
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/GeographyAPI/GetCities?stateID=" + stateID);
            List<CityModel> CityModelList = JsonConvert.DeserializeObject<List<CityModel>>(response);
            return Json(CityModelList, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> VerifyZip(string zipCode)
        {
            string response = await WebApiHelper.HttpClientRequestResponseGet("api/GeographyAPI/VerifyZipCode?zipCode=" + zipCode);
            GeoModel geoModel = JsonConvert.DeserializeObject<GeoModel>(response);
            if (geoModel == null)
                return Json(new GeoModel(), JsonRequestBehavior.AllowGet);
            return Json(geoModel, JsonRequestBehavior.AllowGet);
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