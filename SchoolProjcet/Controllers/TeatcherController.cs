using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.TeatcherDTOs;
using SchoolProjcet.Mapper.TeatcherMapping;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeatcherController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TeatcherController()
        {
            _context = new AppDbContext();
            var con = new MapperConfiguration(e => e.AddProfile<TeatcherProfail>());
            _mapper = con.CreateMapper();
        }

        [HttpGet]
        public IActionResult gitallTeatcher()
        {
            var t = _context.Teachers.Include(e => e.Department).ToList();
            List<TeatcherDTO> teatcherDTOs = _mapper.Map<List<TeatcherDTO>>(t);
            //foreach (var te in t)
            //{
            //    var n = new TeatcherDTO()
            //    {
            //        Email = te.Email,
            //        FullName = te.FirstName + " " + te.LastName,
            //        DepartmentName = te.Department.Name,
            //        Id = te.Id
            //    };
            //    teatcherDTOs.Add(n);
            //}
            return Ok(teatcherDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GitTByID(int id)
        {
            var x = _context.Teachers.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
            if (x == null)
            {
                return NotFound();
            }
            //var t = new TeatcherDTO()
            //{
            //    Email = x.Email,
            //    FullName = x.FirstName + " " + x.LastName,
            //    DepartmentName = x.Department.Name,
            //    Id = x.Id
            //};
            var t = _mapper.Map<TeatcherDTO>(x);
            return Ok(t);
        }

        [HttpDelete]
        public IActionResult DeleteTeatcher(int id)
        {
            var t = _context.Teachers.Find(id);
            if (t == null) { return NotFound(); }
            _context.Teachers.Remove(t);
            _context.SaveChanges();
            return Ok();


        }

        [HttpPost]
        public IActionResult AddTeatcher(CreateTeatcherDTO createTeatcherDTO)
        {
            if (createTeatcherDTO == null)
            {
                return BadRequest("Not Valid data");
            }
            //var t = new Teacher()
            //{
            //    Email = createTeatcherDTO.Email,
            //    FirstName = createTeatcherDTO.FullName,
            //    LastName=createTeatcherDTO.lastName,
            //    DepartmentId=createTeatcherDTO.DepartmentId,
            //};
            var t = _mapper.Map<Teacher>(createTeatcherDTO);
            _context.Teachers.Add(t);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GitTByID), new { id = t.Id }, createTeatcherDTO);
        }
        [HttpPut]
        public IActionResult Update(int id, UpdateTeatcherDTO updateTeatcherDTO)
        {
            if (updateTeatcherDTO == null)
            {
                return BadRequest("not Valied data");
            }


            var x = _context.Teachers.FirstOrDefault(e => e.Id == id);
            if (x == null)
            {
                return NotFound();
            }
            _mapper.Map(updateTeatcherDTO, x);
            //x.FirstName=updateTeatcherDTO.FullName;
            //x.LastName = updateTeatcherDTO.lastName;
            //x.Email=updateTeatcherDTO.Email;



            _context.SaveChanges();
            return Ok();

        }


    }
}
