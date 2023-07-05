using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Server.Models
{
    public class QuestionsModel
    {
        [Key]
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public int QuestionaireID { get; set; }
        public string Company { get; set; }
        public bool isDeleted { get; set; }
    }
}
