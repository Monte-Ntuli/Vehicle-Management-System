namespace BlazorApp1.Server.Models
{
    public class ReportModel
    {
        public int ReportID { get; set; }
        public int EmployeeID { get; set; }
        public int VehicleID { get; set; }
        public int QuestionaireID { get; set; }
        public string Company { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
