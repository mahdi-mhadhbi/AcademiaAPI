using Educational.Core.Models;
using Educational.Core.Repositories;
using Educational.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<Student> Student { get; private set; }
        public IRepository<Course> Course { get; private set; }
        public IRepository<Exam> Exam { get; private set; }
        public IRepository<Question> Question { get; private set; }
        public IRepository<Options> Options { get; private set; }
        public IRepository<Lesson> Lesson { get; private set; }
        public IRepository<Grade> Grade { get; private set; }
        public IRepository<Enrollment> Enrollment { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Student = new Repository<Student>(_context);
            Enrollment = new Repository<Enrollment>(_context);
            Grade = new Repository<Grade>(_context);
            Lesson = new Repository<Lesson>(_context);
            Options = new Repository<Options>(_context);
            Question = new Repository<Question>(_context);
            Exam = new Repository<Exam>(_context);
            Course = new Repository<Course>(_context);
        }
        public AppDbContext GetContext()
        {
            return _context;
        }
        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
