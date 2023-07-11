using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Server.Entities
{
    public class VehicleMakeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int VehicleMakeID { get; set; }
        public string VehicleMakeTitle { get; set; }
        public string Company { get; set; }
        public bool isDeleted { get; set; }
    }
}
