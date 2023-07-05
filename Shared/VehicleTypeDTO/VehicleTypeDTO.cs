using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp1.Shared.VehicleTypeDTO
{
    public class VehicleTypeDTO
    {
        public int VehicleTypeID { get; set; }
        public string VehicleTypeTitle { get; set; }
        public string Company { get; set; }
        public int QuestionaireID { get; set; }
        public bool hasQuestionaire { get; set; }
        public bool isDeleted { get; set; }
    }
}
