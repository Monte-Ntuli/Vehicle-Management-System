namespace BlazorApp1.Shared.RequestModels
{
    public class CreateVehicleRM
    {
        public string VehicleReg { get; set; }
        public string VinNumber { get; set; }
        public int VehicleModelType { get; set; }
        public int VehicleTypeID { get; set; }
        public string Company { get; set; }
    }
}
