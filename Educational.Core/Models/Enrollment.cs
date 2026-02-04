using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Models
{
    public class Enrollment
    {

        public Guid Id { get; set; }
        public DateTime EnrollAt { get; set; } = DateTime.Now;
        [Required]
        public Status Status { get; set; }
        public Student Student { get; set; }
        [ForeignKey(nameof(Student))]
        public Guid StudentId { get; set; }
        public Course Course { get; set; }
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
    }
}
