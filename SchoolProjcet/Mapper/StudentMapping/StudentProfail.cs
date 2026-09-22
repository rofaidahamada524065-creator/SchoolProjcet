using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.StudentDTOs;
namespace SchoolProjcet.Mapper.StudentMapping
{
    public class StudentProfail : Profile   
    {
        public StudentProfail()
        {
            CreateMap<Student, StudentDTO>().ForMember(e => e.FullName, op => op.MapFrom(src => $"{src.FirstName}{src.LastName}")).ReverseMap();

            CreateMap<CreateStudenDTO,Student>().ReverseMap();
            CreateMap<UpdateStudentDTO,Student>().ReverseMap();
        
        
        }
    }
}
