using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Server.Entities
{
    public class VehicleModelTypeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int VehicleModelTypeID { get; set; }
        public string VehicleModelTitle { get; set; }
        public string Company { get; set; }
        public bool isDeleted { get; set; }
    }
}
