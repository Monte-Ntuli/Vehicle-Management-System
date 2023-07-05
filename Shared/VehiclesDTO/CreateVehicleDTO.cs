using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp1.Shared.VehiclesDTO
{
    public class CreateVehicleDTO
    {
        public string VehicleReg { get; set; }
        public string VinNumber { get; set; }
        public int VehicleModelType { get; set; }
        public int VehicleTypeID { get; set; }
        public string Company { get; set; }
    }
}
