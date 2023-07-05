namespace BlazorApp1.Shared.RequestModels
{
    public class UpdateVehicleRM
    {
        public int VehicleID { get; set; }
        public string VehicleReg { get; set; }
        public string VinNumber { get; set; }
        public int VehicleModelType { get; set; }
        public int VehicleTypeID { get; set; }
    }
}
