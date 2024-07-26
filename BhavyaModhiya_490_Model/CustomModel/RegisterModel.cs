using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Model.CustomModel
{
    public class RegisterModel
    {
        public int SellerID { get; set; }

        [Required(ErrorMessage = "FirstName is Required")]
        [MaxLength(length: 15, ErrorMessage = "Length must be less than 15 characters")]
        [MinLength(length: 3, ErrorMessage = "Length must be greater than 3 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is Required")]
        [MaxLength(length: 15, ErrorMessage = "Length must be less than 15 characters")]
        [MinLength(length: 3, ErrorMessage = "Length must be greater than 3 characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is Required")]
        [MaxLength(length: 10, ErrorMessage = "Invalid Number")]
        [MinLength(length: 10, ErrorMessage = "Invalid Number")]
        public string PhoneNumber { get; set; }
        public int BikeID { get; set; }

        [Required(ErrorMessage = "Brand is Required")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is Required")]
        public string Models { get; set; }
        public byte Years { get; set; }

        [Required(ErrorMessage = "Kms Driven is Required")]
        public int kilometresDriven { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
