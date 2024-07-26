using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BhavyaModhiya_490_Model.CustomModel
{
    public class UserModel
    {
        public int? UserID { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "LastName is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is Required")]
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Select atleast one Country")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Select atleast one State")]
        public int StateID { get; set; }

        [Required(ErrorMessage = "Select atleast one City")]
        public int CityID { get; set; }

        [Required(ErrorMessage = "ZipCode is Required")]
        public string ZipCode { get; set; }

        public HttpPostedFileBase ProfilePictureFile { get; set; } = null;
        public string ProfilePicture { get; set; }
        public string Password { get; set; } = null;
    }
}
