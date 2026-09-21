using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.StudentDTOs;
using SchoolProjcet.Mapper.StudentMapping;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentController(AppDbContext context)
        {
            _context = context;
            var con = new MapperConfiguration(e => e.AddProfile<StudentProfail>());
            _mapper = con.CreateMapper();
        }
        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _context.Students.Include(e=>e.ClassRoom).ToList();
            List<StudentDTO> result = _mapper.Map<List<StudentDTO>>(students);
            //foreach (var student in students)
            //{
            // var studentDTO = new StudentDTO
            //    {
            //     Id = student.Id,
            //     ClassRoomName = student.ClassRoom.Name,
            //     FullName = $"{student.FirstName} {student.LastName}",
            //     Email = student.Email,
            //     PhoneNumber = student.PhoneNumber

            // };
            //    result.Add(studentDTO);
            //}
            return Ok(students);
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudenDTO student)
        {
            if(student == null)
            {
                return BadRequest("Student data is null.");
            }
            //var x=new Student
            //{
            //    FirstName = student.FirstName,
            //    LastName = student.LastName,
            //    Email = student.Email,
            //    PhoneNumber = student.PhoneNumber,
            //    ClassRoomId = student.ClassRoomId
            //};
            var x=_mapper.Map<Student>(student);
           
            _context.Students.Add(x);
            _context.SaveChanges();
            return Ok(student);
        }

        [HttpPut]
        public IActionResult UpdateStudent(UpdateStudentDTO student, int id) 
        {
            var x = _context.Students.Find(id);
            if(x == null)
            {
                return NotFound();
            }
            var s=_mapper.Map<Student>(student);
            _context.Students.Update(s);
            _context.SaveChanges();

            return Ok();
        
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _context.Students.Include(e=>e.ClassRoom).ToList();
            if (student == null)
            {
                return NotFound();
            }
            var x = _mapper.Map<StudentDTO>(student);
            return Ok(student);
        }
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            var t = _context.Students.Find(id);
            if (t == null) { return NotFound(); }
            _context.Students.Remove(t);
            _context.SaveChanges();
            return Ok();


        }
    }
}
