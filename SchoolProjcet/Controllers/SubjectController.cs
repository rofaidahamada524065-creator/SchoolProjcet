using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.SubjectDTOs;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SubjectController()
        {
            _context = new AppDbContext();
        }

        [HttpGet]

        public IActionResult GetAllSubject()
        {
            var subjectsFromDb = _context.Subjects
                .Include(s => s.Teacher)
                .ToList();

            List<SubjectDTO> subjects = new List<SubjectDTO>();

            foreach (var subject in subjectsFromDb)
            {
                SubjectDTO dto = new SubjectDTO()
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    Description = subject.Description,
                    MaxGrade = subject.MaxGrade,
                    NameTeatcher = subject.Teacher.FirstName + " " + subject.Teacher.LastName
                };

                
               
                subjects.Add(dto);
            }

            return Ok(subjects);
        }
        [HttpPost]
        public IActionResult AddSubject(CreateSubjectDTO createSubjectDTO)
        {
            if (createSubjectDTO == null)
            {
                return BadRequest("Data is null");
            }
            var s = new Subject
            {
                Description = createSubjectDTO.Description,
                Name = createSubjectDTO.Name,
                MaxGrade = createSubjectDTO.MaxGrade,
                TeacherId = createSubjectDTO.TeatcherID,
            };
            _context.Subjects.Add(s);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSubject), new { id = s.Id }, createSubjectDTO);
        
        }

        [HttpPut]
        public IActionResult UpdateSubject(int id,UpdateSubjectsDTO updateSubjectsDTO)
        {
            var x = _context.Subjects.Find(id);
            if(x == null)
            {
                return NotFound();
            }

            x.Description = updateSubjectsDTO.Description;
            x.Name = updateSubjectsDTO.Name;
            x.MaxGrade = updateSubjectsDTO.MaxGrade;
            x.TeacherId = updateSubjectsDTO.TeatcherID;
            _context.SaveChanges();
            return Ok(updateSubjectsDTO);
            
        }

        [HttpGet("{id}")]
        public IActionResult GetSubject(int id)
        {
            var subject = _context.Subjects.Find(id);
            var tec=_context.Teachers.ToList();
           
            if (subject == null)
            {
                return NotFound();
            }
            var dto = new SubjectDTO()
            {
                Id = subject.Id,
                Name = subject.Name,
                Description = subject.Description,
                MaxGrade = subject.MaxGrade,
                NameTeatcher = subject.Teacher.FirstName + " " + subject.Teacher.LastName
            };

            return Ok(dto);
        }

        [HttpDelete]
        public IActionResult DeleteSubject(int id)
        {
            var subject = _context.Subjects.Find(id);
            if (subject == null)
            {
                return NotFound();
            }
            _context.Subjects.Remove(subject);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAllSubject), new { id = subject.Id }, subject);
        }

    }
}
