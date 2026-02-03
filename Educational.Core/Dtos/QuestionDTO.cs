using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Dtos
{
    public class QuestionDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int CorrectAnswerOption { get; set; }
        public Guid FKExam { get; set; }
        public string Title_exam { get; set; }
    }
}
