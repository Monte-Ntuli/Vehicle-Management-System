using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp1.Shared.VehicleModelTypeDTO
{
    public class VehicleModelTypeDTO
    {
        public int VehicleModelTypeID { get; set; }
        public string VehicleModelTitle { get; set; }
        public string Company { get; set; }
        public bool isDeleted { get; set; }
    }
}
