using AutoMapper;
using Educational.Core.Dtos;
using Educational.Core.Models;
using Educational.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Educational.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        public readonly IMapper _mapper;

        public EnrollmentController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var enrollments = _context.Enrollment.GetList(
                includes: q => q.Include(e => e.Student)
                                .Include(e => e.Course)
            );

            var result = enrollments.Select(e => _mapper.Map<EnrollmentDTO>(e)).ToList();
            return Ok(result);
        }

        [HttpGet("ByStudent/{studentId}")]
        public IActionResult GetByStudent(Guid studentId)
        {
            var enrollments = _context.Enrollment.GetList(
             includes: q => q.Include(e => e.Student)
                             .Include(e => e.Course)
         );


            var result = enrollments.Select(e => _mapper.Map<EnrollmentDTO>(e)).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult EnrollStudent(Guid studentId, Guid courseId)
        {
            var enrollment = new Enrollment
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                CourseId = courseId,
                Status = Status.Active
            };

            _context.Enrollment.Add(enrollment);
            _context.Complete();

            return Ok(enrollment);
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(Guid id)
        {
            var enrollment = _context.Enrollment.GetById(id);
            if (enrollment == null) return NotFound();

            _context.Enrollment.Remove(enrollment);
            _context.Complete();
            return NoContent();
        }
    }
}
