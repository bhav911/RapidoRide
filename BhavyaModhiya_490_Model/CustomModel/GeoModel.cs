using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Model.CustomModel
{
    public class GeoModel
    {
        public List<CountryModel> CountryList { get; set; }
        public List<StateModel> StateList { get; set; }
        public List<CityModel> CityList { get; set; }
        public int Cid { get; set; }
        public int Sid { get; set; }
        public int Ccid { get; set; }
    }
}
