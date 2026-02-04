using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Models
{
    public class Exam
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [Required]
        public double TotalMarks { get; set; }

        public List<Grade> Grades { get; set; } = new();

        public Course Course { get; set; }
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
