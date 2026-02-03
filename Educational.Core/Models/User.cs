using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Models
{
    public class User : IdentityUser
    {
        public Student Student { get; set; }
    }
}
