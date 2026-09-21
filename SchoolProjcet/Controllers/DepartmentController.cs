using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.DepartmentDTOs;
using SchoolProjcet.DTOs.TeatcherDTOs;
using SchoolProjcet.Mapper.DepartmentMapping;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapp;

        public DepartmentController()
        {
            _context = new AppDbContext();
            var confing = new MapperConfiguration(cef => cef.AddProfile<DepartmentProfail>());

            // Fix: Use the CreateMapper() method to instantiate the IMapper instance
            _mapp = confing.CreateMapper();
        }

        [HttpGet]
        public IActionResult GetallDepartment()
        {
            var d = _context.Departments.ToList();
            List<DepartmentDTO> depart =_mapp.Map<List<DepartmentDTO>>(d);
            if(d==null || d.Count == 0)
            {
                return NotFound("No Department Found");
            }
            //foreach (var de in d)
            //{
            //    var r = new DepartmentDTO()
            //    {
            //        Name = de.Name,
            //        Id = de.Id,
            //        Description = de.Description,
            //    };
            //    depart.Add(r);
            //}
            return Ok(depart);
        }

        [HttpPost]
        public IActionResult AddDepartment(CreateDepartMentDTO department)
        {
            if (department == null)
            {
                return BadRequest("No valid data");
            }
            var d=_mapp.Map<Department>(department);
            //var d = new Department()
            //{
            //    Name = department.Name,

            //    Description = department.Description,
            //};
            _context.Add(d);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = d.Id }, department);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var d = _context.Departments.Find(id);
            if (d == null)
            {
                return NotFound();
            }
            var de=_mapp.Map<DepartmentDTO>(d);
            //var de = new DepartmentDTO()
            //{
            //    Description = d.Description,
            //    Id = d.Id,
            //    Name = d.Name,
            //};

            return Ok(de);
        }

        [HttpPut]
        public IActionResult Update(int id, UpdateDepartmentDTO departmentDTO)
        {
            if (departmentDTO == null)
            {
                return BadRequest("Not Valid Data");
            }
            var x = _context.Departments.Find(id);
            if (x == null)
            {
                return NotFound();
            }
            //source دا المكان الى بخزن فى / destination دا المكان الى باخد منه الدتا
            _mapp.Map(departmentDTO,x);


            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            var x = _context.Departments.Find(id);
            if (x == null)
            {
                return NotFound();
            }
            _context.Departments.Remove(x);
            _context.SaveChanges();
            return Ok();
        }
    }
}
