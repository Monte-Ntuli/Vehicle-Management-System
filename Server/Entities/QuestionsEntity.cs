using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Server.Entities
{
    public class QuestionsEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public int QuestionaireID { get; set; }
        public string Company { get; set; }
        public bool isDeleted { get; set; }
    }
}
