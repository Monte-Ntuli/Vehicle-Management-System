using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Server.Models
{
    public class VehicleModel
    {
        [Key]public int VehicleID { get; set; }
        public string VehicleReg { get; set; }
        public string VinNumber { get; set; }
        public int VehicleModelType { get; set; }
        public int VehicleTypeID { get; set; }
        public string Company { get; set; }
        public bool isDeleted { get; set; }
    }
}
