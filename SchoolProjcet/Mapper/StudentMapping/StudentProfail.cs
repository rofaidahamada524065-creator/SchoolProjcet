using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.StudentDTOs;
namespace SchoolProjcet.Mapper.StudentMapping
{
    public class StudentProfail : Profile   
    {
        public StudentProfail() 
        {
            CreateMap<Student,StudentDTO>().ReverseMap();
            CreateMap<CreateStudenDTO,Student>().ReverseMap();
            CreateMap<UpdateStudentDTO,Student>().ReverseMap();
        
        
        }
    }
}
