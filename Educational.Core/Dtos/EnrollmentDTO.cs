using Educational.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Dtos
{
    public class EnrollmentDTO
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime EnrollAt { get; set; } = DateTime.Now;
        public Status Status { get; set; }
        public Guid FKCourse { get; set; }
        public string Title_course { get; set; }
        public Guid FKStudent { get; set; }
        public string Name_student { get; set; }
    }
}
