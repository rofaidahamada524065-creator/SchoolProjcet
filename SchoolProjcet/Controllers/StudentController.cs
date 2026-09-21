using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.StudentDTOs;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _context.Students.Include(e=>e.ClassRoom).ToList();
            List<StudentDTO> result = new List<StudentDTO>();
            foreach (var student in students)
            {
             var studentDTO = new StudentDTO
                {
                 Id = student.Id,
                 ClassRoomName = student.ClassRoom.Name,
                 FullName = $"{student.FirstName} {student.LastName}",
                 Email = student.Email,
                 PhoneNumber = student.PhoneNumber

             };
                result.Add(studentDTO);
            }
            return Ok(students);
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudenDTO student)
        {
            if(student == null)
            {
                return BadRequest("Student data is null.");
            }
            var x=new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                ClassRoomId = student.ClassRoomId
            };
           
            _context.Students.Add(x);
            _context.SaveChanges();
            return Ok(student);
        }

    }
}
