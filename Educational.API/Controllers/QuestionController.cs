using AutoMapper;
using Educational.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Educational.Core.Dtos;
using Educational.Core.Models;

namespace Educational.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        public readonly IMapper _mapper;

        public QuestionController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        #region Standard CRUD methods

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<QuestionDTO>> GetAllQuestions()
        {
            var questions = _context.Question.GetList();

            var result = questions.Select(c => _mapper.Map<QuestionDTO>(c))
                            .ToList();

            return Ok(result);

        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Add(Question question)
        {
            _context.Question.Add(question);
            _context.Complete();

            return Ok(question);
        }
        [HttpPut]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(Question question)
        {
            _context.Question.Update(question);
            _context.Complete();

            return Ok(question);
        }
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        public ActionResult<Question> Delete(Guid id)
        {
            var question = _context.Question.Get(c => c.Id == id);

            _context.Question.Remove(question);
            _context.Complete();
            return Ok("Delete done");
        }
        #endregion
    }
}