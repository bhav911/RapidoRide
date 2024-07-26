using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BhavyaModhiya_490_Model.CustomModel
{
    public class FilterModel
    {
        public string yearsOfUsage { get; set; } = "1,2";
        public string kmDriven { get; set; } = "1000,3000";
        public string brand { get; set; } = "*";
        public int fetchPage { get; set; } = 1;
    }
}
