using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Model.CustomModel
{
    public class BikeModel
    {
        public int SellerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int BikeID { get; set; }
        public string Brand { get; set; }
        public string Models { get; set; }
        public byte Years { get; set; }
        public int KilometresDriven { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsFavorite { get; set; }
    }
}
