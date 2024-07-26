using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Model.CustomModel
{
    public class CityModel
    {
        public int CityID { get; set; }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public Nullable<int> StateID { get; set; }
    }
}
