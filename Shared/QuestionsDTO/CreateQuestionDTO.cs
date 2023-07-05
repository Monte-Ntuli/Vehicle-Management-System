using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp1.Shared.QuestionsDTO
{
    public class CreateQuestionDTO
    {
        public string Title { get; set; }
        public int QuestionaireID { get; set; }
        public string Company { get; set; }
    }
}
