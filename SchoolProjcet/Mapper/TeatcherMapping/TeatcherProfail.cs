using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.TeatcherDTOs;
namespace SchoolProjcet.Mapper.TeatcherMapping
{
    public class TeatcherProfail : Profile
    {
        public TeatcherProfail() { 
        
            CreateMap<Teacher,TeatcherDTO>().ReverseMap();
            CreateMap<CreateTeatcherDTO,Teacher>().ReverseMap();
            CreateMap<UpdateTeatcherDTO,Teacher>().ReverseMap();

        
        }
    }
}
