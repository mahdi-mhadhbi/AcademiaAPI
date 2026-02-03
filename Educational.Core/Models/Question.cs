using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Models
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public List<Options> Options { get; set; } = new List<Options>();
        [Required]
        [Range(1, 4)]
        public int CorrectAnswerOption { get; set; }
        public Exam Exam { get; set; }
        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }
    }
}
