using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.AppContext;
using School.Models;
using SchoolProjcet.DTOs.ClassRoomDTOs;
using SchoolProjcet.Mapper.ClassRoomMapping;

namespace SchoolProjcet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ClassRoomController()
        {
            _context = new AppDbContext();
            var gsf = new MapperConfiguration(e => e.AddProfile<ClassRoomProfail>());
            _mapper=gsf.CreateMapper();
        }
        [HttpGet]
        public IActionResult GetAllClassRoom()
        {
            var c = _context.ClassRooms.ToList();
            List<ClassRoomDTO> list = _mapper.Map<List<ClassRoomDTO>>(c);
            //foreach (var room in c)
            //{
            //    var d = new ClassRoomDTO()
            //    {
            //        Capacity = room.Capacity,
            //        GradeLevel = room.GradeLevel,
            //        Id = room.Id,
            //        Name = room.Name,
            //    };
            //    list.Add(d);
            //}
            return Ok(list);
        }

        [HttpGet("{id}")]
        public IActionResult GetClassRoomById(int id)
        {
            var n=_context.ClassRooms.FirstOrDefault(r => r.Id == id);
            if (n == null)
            {
                return NotFound();
            }
            //var d = new ClassRoomDTO()
            //    {
            //        Capacity = n.Capacity,
            //        GradeLevel = n.GradeLevel,
            //        Id = n.Id,
            //        Name = n.Name,

            //   };
            var d=_mapper.Map<ClassRoomDTO>(n);
            return Ok(d);
            
        }

        [HttpDelete]
        public IActionResult DeleteClassRoom(int id)
        {
            var x = _context.ClassRooms.Find(id);
            if (x == null)
            {
                return NotFound();
            }
            _context.ClassRooms.Remove(x);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult AddClassRoom(CreateClassRoomDTO classRoomDTO)
        {
            if (classRoomDTO == null)
            {
                return BadRequest("NortValide Data");
            }
            //var d = new ClassRoom()
            //{
            //    Capacity = classRoomDTO.Capacity,
            //    GradeLevel = classRoomDTO.GradeLevel,
            //    Name = classRoomDTO.Name,
            //};
            var d=_mapper.Map<ClassRoom>(classRoomDTO);
            _context.ClassRooms.Add(d);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetClassRoomById), new { id = d.Id }, classRoomDTO);

        }

        [HttpPut]
        public IActionResult Update(int id,UpdateClassRoomDTO classRoomDTO)
        {
            if(classRoomDTO == null)
            {
                return BadRequest("Data not valid");
            }
            var x = _context.ClassRooms.Find(id);
            if (x == null)
            {
                return NotFound();
            }
            //x.Name = classRoomDTO.Name;
            // x.Capacity = classRoomDTO.Capacity;
            // x.GradeLevel = classRoomDTO.GradeLevel;
            _mapper.Map(classRoomDTO, x);
            _context.SaveChanges();
            return Ok(classRoomDTO);
        }

    }
}
