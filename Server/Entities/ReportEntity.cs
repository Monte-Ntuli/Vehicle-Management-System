using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Server.Entities
{
    public class ReportEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int ReportID { get; set; }
        public int GroupID { get; set; }
        public int QuestionID { get; set; }
        public int QuestionaireID { get; set; }
        public int EmployeeID { get; set; }
        public int VehicleID { get; set; }
        public string Company { get; set; }
        public string Answer { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
