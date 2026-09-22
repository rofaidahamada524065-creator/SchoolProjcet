using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.TeatcherDTOs;
namespace SchoolProjcet.Mapper.TeatcherMapping
{
    public class TeatcherProfail : Profile
    {
        public TeatcherProfail() { 
        
            CreateMap<Teacher,TeatcherDTO>().ForMember(e=>e.FullName,m=>m.MapFrom(s=>s.FirstName+" "+s.LastName)).ReverseMap();
            CreateMap<Teacher, CreateTeatcherDTO>().ForMember(e => e.FullName, m => m.MapFrom(s => s.FirstName + " " + s.LastName)).ReverseMap();
            CreateMap<Teacher, UpdateTeatcherDTO>().ForMember(e => e.FullName, m => m.MapFrom(s => s.FirstName + " " + s.LastName)).ReverseMap();

        
        }
    }
}
