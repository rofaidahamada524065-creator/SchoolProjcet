using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
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

        public StudentController()
        {
            _context =new AppDbContext();
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
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudenDTO studentDTo)
        {
            if(studentDTo == null)
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
            var x=_mapper.Map<Student>(studentDTo);
           
            _context.Students.Add(x);
            _context.SaveChanges();
            return Ok(studentDTo);
        }

        [HttpPut]
        public IActionResult UpdateStudent(UpdateStudentDTO student, int id) 
        {
            var x = _context.Students.Find(id);
            if(x == null)
            {
                return NotFound();
            }
            _mapper.Map(student, x);
            _context.SaveChanges();

            return Ok();
        
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _context.Students.Include(e=>e.ClassRoom).FirstOrDefault(e => e.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            var x = _mapper.Map<StudentDTO>(student);
            return Ok(x);
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

        [HttpGet("OrderBy")]
        public IActionResult OrderBy()
        {
            var x = _context.Students
                .OrderBy(e => e.LastName).Select(e=>new
                {
                    e.FirstName,
                    e.LastName,
                    e.Email,
                    e.DateOfBirth
                })
                .ToList();

            return Ok(x);
        }

        [HttpGet("filter")]
        public IActionResult AllStudent(int classRoomId, decimal minGrade)
        {
            var students = _context.Students
                .Where(s => s.ClassRoomId == classRoomId)
                .Where(s => s.Enrollments.Any(e => e.Grade >= minGrade))
                .ToList();

            return Ok(students);
        }

        [HttpGet("SpecialStudents")]
        public IActionResult gitstudent(int classRoom)
        {
            var x = _context.Students.OrderBy(e => e.Id).Where(e => e.ClassRoomId == classRoom).Select(e => new
            {
                e.FirstName,
                e.LastName,
               
                e.PhoneNumber,
                e.DateOfBirth
            }).FirstOrDefault();
            return Ok(x);

        }

        [HttpGet("first student")]
        public IActionResult GetFrist(int classRoom)
        {
            
            var x = _context.Students.Where(e => e.ClassRoomId == classRoom).OrderBy(e=>e.Id)
                .FirstOrDefault();
            if(x== null)
            {
                return NotFound();
            }
            
            return Ok(x);
        }
        [HttpGet("Maching Email")]
        public IActionResult MachingEmail(string email)
        {
            var x = _context.Students.Where(e => e.Email == email).FirstOrDefault();
            if (x == null)
            {
                return NotFound();
            }
            return Ok(x);
        }


        [HttpGet("UaniceStudetwhithEmail")]
        public IActionResult UaniceStudet(string Email)
        {
            var x = _context.Students.Where(e => e.Email == Email).Distinct();
            return Ok(x);
        }


    }
}
