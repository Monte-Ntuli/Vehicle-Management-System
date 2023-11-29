using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp1.Server.Entities
{
    public class AnswerEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int AnswerID { get; set; }
        public int QuestionID { get; set; }
        public string AnswerTitle { get; set; }
        public int ReportID { get; set; }

    }
}
