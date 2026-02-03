using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public  User User { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        [Range(1, 3)]
        public int Level { get; set; }
        [MaxLength(100)]
        public string Governorate { get; set; }
    }
}
