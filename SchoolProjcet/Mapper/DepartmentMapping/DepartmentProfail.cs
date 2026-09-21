using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.DepartmentDTOs;

namespace SchoolProjcet.Mapper.DepartmentMapping
{
    public class DepartmentProfail : Profile
    {
        public DepartmentProfail()
        {
            CreateMap<Department,DepartmentDTO>().ReverseMap();
            CreateMap<CreateDepartMentDTO,Department>().ReverseMap();
            CreateMap<UpdateDepartmentDTO, Department>().ReverseMap();

        }
    }
}
