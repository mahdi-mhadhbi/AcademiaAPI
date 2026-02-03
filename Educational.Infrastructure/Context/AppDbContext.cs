using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Educational.Core.Models;
namespace Educational.Infrastructure.Context
{
    public  class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Student { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Course> Course { get; set; }


        public int Complete()
        {
            return SaveChanges();
        }

    }
}
