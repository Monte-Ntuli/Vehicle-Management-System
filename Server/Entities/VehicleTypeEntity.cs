using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Server.Entities
{
    public class VehicleTypeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int VehicleTypeID { get; set; }
        public string VehicleTypeTitle { get; set; }
        public string Company { get; set; }
        public int QuestionaireID { get; set; }
        public bool hasQuestionaire { get; set; }
        public bool isDeleted { get; set; }
    }
}
