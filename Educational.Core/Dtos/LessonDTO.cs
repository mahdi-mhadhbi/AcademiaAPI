using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Dtos
{
    public class LessonDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string name { get; set; }
        public string URL { get; set; }
        public int Order { get; set; }
        public int DurationMinutes { get; set; }
        public Guid FKCourse { get; set; }
        public string Title_course { get; set; }

    }
}
