using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Dtos
{
    public class ExamDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double TotalMarks { get; set; }
        public Guid FKCourse { get; set; }
        public string Title_course { get; set; }
    }
}
