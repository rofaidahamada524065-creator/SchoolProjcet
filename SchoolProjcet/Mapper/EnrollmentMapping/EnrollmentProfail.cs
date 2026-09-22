using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.EnrollmentDTOs;
namespace SchoolProjcet.Mapper.EnrollmentMapping
{
    public class EnrollmentProfail : Profile
    {
        public EnrollmentProfail()
        {
            CreateMap<Enrollment, EnrollmentDTO>()
            .ForMember(dest => dest.StudentName,
             opt => opt.MapFrom(src => src.Student.FirstName +" "+src.Student.LastName))
            .ForMember(dest => dest.SubjectNamme,
                 opt => opt.MapFrom(src => src.Subject.Name)).ReverseMap();

            CreateMap<Enrollment,CreateEnrollment>().ReverseMap();
            CreateMap<UpdateEnrollment,Enrollment>().ReverseMap();
           

        
        
        }
    }
}
