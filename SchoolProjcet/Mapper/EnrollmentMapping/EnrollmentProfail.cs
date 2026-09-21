using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.EnrollmentDTOs;
namespace SchoolProjcet.Mapper.EnrollmentMapping
{
    public class EnrollmentProfail : Profile
    {
        public EnrollmentProfail() 
        {
            CreateMap<Enrollment,EnrollmentDTO>().ReverseMap();
            CreateMap<Enrollment,CreateEnrollment>().ReverseMap();
            CreateMap<UpdateEnrollment,Enrollment>().ReverseMap();
           

        
        
        }
    }
}
