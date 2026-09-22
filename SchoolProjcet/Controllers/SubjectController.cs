using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.SubjectDTOs;
using SchoolProjcet.Mapper.SubjectMapping;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public SubjectController()
        {
            _context = new AppDbContext();
            var dgf = new MapperConfiguration(e => e.AddProfile<SubjectProfail>());
            _mapper=dgf.CreateMapper();
        }

        [HttpGet]

        public IActionResult GetAllSubject()
        {
            var subjectsFromDb = _context.Subjects
                .Include(s => s.Teacher)
                .ToList();

            List<SubjectDTO> subjects = _mapper.Map<List<SubjectDTO>>(subjectsFromDb);

            //foreach (var subject in subjectsFromDb)
            //{
            //    SubjectDTO dto = new SubjectDTO()
            //    {
            //        Id = subject.Id,
            //        Name = subject.Name,
            //        Description = subject.Description,
            //        MaxGrade = subject.MaxGrade,
            //        NameTeatcher = subject.Teacher.FirstName + " " + subject.Teacher.LastName
            //    };

                
               
            //    subjects.Add(dto);
            //}

            return Ok(subjects);
        }
        [HttpPost]
        public IActionResult AddSubject(CreateSubjectDTO createSubjectDTO)
        {
            if (createSubjectDTO == null)
            {
                return BadRequest("Data is null");
            }
            //var s = new Subject
            //{
            //    Description = createSubjectDTO.Description,
            //    Name = createSubjectDTO.Name,
            //    MaxGrade = createSubjectDTO.MaxGrade,
            //    TeacherId = createSubjectDTO.TeatcherID,
            //};
            var s = _mapper.Map<Subject>(createSubjectDTO);
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

            //x.Description = updateSubjectsDTO.Description;
            //x.Name = updateSubjectsDTO.Name;
            //x.MaxGrade = updateSubjectsDTO.MaxGrade;
            //x.TeacherId = updateSubjectsDTO.TeatcherID;
           var s = _mapper.Map(updateSubjectsDTO, x); 
            _context.Subjects.Update(s);
            _context.SaveChanges();
            return Ok();
            
        }

        [HttpGet("{id}")]
        public IActionResult GetSubject(int id)
        {
            var subject = _context.Subjects.Include(e=>e.Teacher).ToList();
            var tec=_context.Subjects.Find(id);
            
           
            if (subject == null)
            {
                return NotFound();
            }
            //var dto = new SubjectDTO()
            //{
            //    Id = subject.Id,
            //    Name = subject.Name,
            //    Description = subject.Description,
            //    MaxGrade = subject.MaxGrade,
            //    NameTeatcher = subject.Teacher.FirstName + " " + subject.Teacher.LastName
            //};
            var x=_mapper.Map<SubjectDTO>(tec);

            return Ok(x);
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
