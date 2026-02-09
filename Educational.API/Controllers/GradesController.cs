using AutoMapper;
using Educational.Core.Dtos;
using Educational.Core.Models;
using Educational.Core.Repositories;
using Educational.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Educational.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IUnitOfWork _context;
        public readonly IMapper _mapper;

        public GradesController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("Submit")]
        [Authorize]
        public  IActionResult SubmitExam(List<Question> answers, Guid examId, Guid studentId)
        {
               var exam = _context.Exam.GetById(examId);

            var student =  _context.Student.GetById(studentId);


            int score = 0;

            foreach (var answer in answers)
            {
                var question = exam.Questions.FirstOrDefault(q => q.Id == answer.Id);
                if (question != null && question.CorrectAnswerOption == answer.CorrectAnswerOption)
                    score++;
            }
            var grade = new Grade()
            {
                ExamId = examId,
                StudentId = studentId,
                Score = score
            };

            _context.Grade.Add(grade);

            _context.Complete();

            return Ok(score);
        }



    }
}
