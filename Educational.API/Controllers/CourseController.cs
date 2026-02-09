using AutoMapper;
using Educational.Core.Models;
using Educational.Core.Repositories;
using Educational.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Educational.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        public readonly IMapper _mapper;

        public CourseController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Standard CRUD methods
        [HttpGet]
        public IEnumerable<Course> GetAllCourses()
        {
            var courses = _context.Course.GetList();

            return courses.ToList();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Add(Course course)
        {
             _context.Course.Add(course);
                        _context.Complete();
           
            return Ok(course);
        }
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(Course course)
        {
            _context.Course.Update(course);
            _context.Complete();    

            return Ok(course);
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public ActionResult<Course> Delete(Guid id)
        {
            var course = _context.Course.Get(c => c.Id == id);

            _context.Course.Remove(course);
            _context.Complete();
            return Ok("Delete done");
        }
        #endregion



    }
}
