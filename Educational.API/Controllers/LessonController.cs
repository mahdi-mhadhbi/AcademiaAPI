using AutoMapper;
using Educational.Core.Dtos;
using Educational.Core.Models;
using Educational.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Educational.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        public readonly IMapper _mapper;

        public LessonController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #region Standard CRUD methods
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<LessonDTO>> GetAllLessons()
        {
            var lessons = _context.Lesson.GetList();

            var result = lessons.Select(c => _mapper.Map<LessonDTO>(c))
                            .ToList();

            return Ok(result);

        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Add(Lesson lesson)
        {
            _context.Lesson.Add(lesson);
            _context.Complete();

            return Ok(lesson);
        }
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(Lesson lesson)
        {
            _context.Lesson.Update(lesson);
            _context.Complete();

            return Ok(lesson);
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public ActionResult<Lesson> Delete(Guid id)
        {
            var lesson = _context.Lesson.Get(c => c.Id == id);

            _context.Lesson.Remove(lesson);
            _context.Complete();
            return Ok("Delete done");
        }
        #endregion

    }
}
