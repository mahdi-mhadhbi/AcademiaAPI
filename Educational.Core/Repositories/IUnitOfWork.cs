using Educational.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Student> Student { get; }
        IRepository<Course> Course { get; }
        IRepository<Exam> Exam { get; }
        IRepository<Question> Question { get; }
        IRepository<Options> Options { get; }
        IRepository<Lesson> Lesson { get; }
        IRepository<Grade> Grade { get; }
        IRepository<Enrollment> Enrollment { get; }

        int Complete();
    }
}

