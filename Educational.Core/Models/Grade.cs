using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Models
{
    public class Grade
    {
        public Guid Id { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Score { get; set; }
        public Student Student { get; set; }
        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }
        public Exam Exam { get; set; }
        [ForeignKey(nameof(Exam))]
        public Guid ExamId { get; set; }
    }
}
