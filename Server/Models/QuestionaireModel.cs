using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Server.Models
{
    public class QuestionaireModel
    {
        [Key]public int QuestionaireID { get; set; }
        public string Title { get; set; }
        public int VehicleTypeID { get; set; }
        public string Company { get; set; }
    }
}
