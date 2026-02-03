using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Dtos
{
    public class OptionDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
        public Guid FKQuestion { get; set; }
        public string Text_question { get; set; }

    }
}
