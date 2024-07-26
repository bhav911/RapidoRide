using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Model.CustomModel
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Email is Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MaxLength(length: 6, ErrorMessage = "Length must be less than 6 characters")]
        [MinLength(length: 3, ErrorMessage = "Length must be greater than 3 characters")]
        public string Password { get; set; }
    }
}
