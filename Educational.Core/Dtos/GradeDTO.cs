using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Dtos
{
    public class GradeDTO
    {
        [Key]
        public Guid Id { get; set; }
        public double Score { get; set; }
        public Guid FKExam { get; set; }
        public string Title_exam { get; set; }
        public Guid FKStudent { get; set; }
        public string Name_student { get; set; }
    }
}
