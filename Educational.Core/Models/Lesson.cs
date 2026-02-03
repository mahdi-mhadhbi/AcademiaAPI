using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        public string URL { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Order { get; set; }
        [Range(1, int.MaxValue)]
        public int DurationMinutes { get; set; }
        public Course Course { get; set; }
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
    }
}
