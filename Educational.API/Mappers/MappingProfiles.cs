using AutoMapper;
using Educational.Core.Dtos;
using Educational.Core.Models;
namespace Educational.API.Mappers

{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {

            CreateMap<Student, StudentDTO>();

            CreateMap<Enrollment, EnrollmentDTO>()
                .ForMember(d => d.FKStudent, o => o.MapFrom(s => s.StudentId))
                .ForMember(d => d.FKCourse, o => o.MapFrom(s => s.CourseId))
                .ForMember(d => d.Name_student, o => o.MapFrom(s => s.Student.Name))
                .ForMember(d => d.Title_course, o => o.MapFrom(s => s.Course.Title))
                .ReverseMap();

            CreateMap<Exam, ExamDTO>()

           .ForMember(d => d.Title_course, i => i.MapFrom(src => src.Course.Title))
           .ReverseMap();


            CreateMap<Question, QuestionDTO>()

           .ForMember(d => d.Title_exam, i => i.MapFrom(src => src.Exam.Title))
           .ReverseMap();


            CreateMap<Options, OptionDTO>()

           .ForMember(d => d.Text_question, i => i.MapFrom(src => src.Question.Text))
           .ReverseMap();

            CreateMap<Lesson, LessonDTO>()

           .ForMember(d => d.Title_course, i => i.MapFrom(src => src.Course.Title))
           .ReverseMap();

            CreateMap<Grade, GradeDTO>()

           .ForMember(d => d.Name_student, i => i.MapFrom(src => src.Student.Name))
           .ForMember(d => d.Title_exam, i => i.MapFrom(src => src.Exam.Title))
           .ReverseMap();
        }
    }
}
