using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.EnrollmentDTOs;
using SchoolProjcet.Mapper.EnrollmentMapping;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public EnrollmentController()
        {
            _context = new AppDbContext();
            var con=new MapperConfiguration(e=>e.AddProfile<EnrollmentProfail>());
            _mapper = con.CreateMapper();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var x = _context.Enrollments.Include(e => e.SubjectId).Include(e => e.StudentId).ToList();
            List<EnrollmentDTO> list = _mapper.Map<List<EnrollmentDTO>>(x);
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Create(CreateEnrollment enrollmentDTO)
        {
            if (enrollmentDTO == null)
            {
                return BadRequest("The Data Not Found");
            }
            var c = _mapper.Map<Enrollment>(enrollmentDTO);
            _context.Enrollments.Add(c);
            _context.SaveChanges();
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(EnrollmentDTO enrollmentDTO, int id)
        {
            var x = _context.Enrollments.Find(id);
            if (x == null)
            {
                return NotFound();
            }
            var s=_mapper.Map<Enrollment>(enrollmentDTO);
            _context.Enrollments.Update(s);
            _context.SaveChanges();
            return Ok();


        }
        [HttpDelete]
        public IActionResult DeleteEnrollment(int id)
        {
            var t = _context.Enrollments.Find(id);
            if (t == null) { return NotFound(); }
            _context.Enrollments.Remove(t);
            _context.SaveChanges();
            return Ok();


        }

    }
}
