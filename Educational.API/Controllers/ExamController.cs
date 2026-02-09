using AutoMapper;
using Educational.Core.Dtos;
using Educational.Core.Models;
using Educational.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Educational.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        public readonly IMapper _mapper;

        public ExamController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Standard CRUD methods

        [HttpGet("GetAll")]
        public ActionResult< IEnumerable<ExamDTO>> GetAllExams()
        {
            var exams = _context.Exam.GetList();

            var result = exams.Select(c => _mapper.Map<ExamDTO>(c))
                              .ToList();

            return Ok(result);

        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Add(Exam exam)
        {
            _context.Exam.Add(exam);
            _context.Complete();

            return Ok(exam);
        }
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(Exam exam)
        {
            _context.Exam.Update(exam);
            _context.Complete();

            return Ok(exam);
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public ActionResult<Exam> Delete(Guid id)
        {
            var exam = _context.Exam.Get(c => c.Id == id);

            _context.Exam.Remove(exam);
            _context.Complete();
            return Ok("Delete done");
        }
#endregion  

    }
}
