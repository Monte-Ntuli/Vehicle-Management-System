using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Server.Models
{
    public class VehicleTypeModel
    {
        [Key]
        public int VehicleTypeID { get; set; }
        public string VehicleTypeTitle { get; set; }
        public string Company { get; set; }
        public int QuestionaireID { get; set; }
        public bool hasQuestionaire { get; set; }
        public bool isDeleted { get; set; }
    }
}
