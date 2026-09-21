using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.SubjectDTOs;
namespace SchoolProjcet.Mapper.SubjectMapping
{
    public class SubjectProfail :Profile
    {
        public SubjectProfail()
        {
            CreateMap<Subject, SubjectDTO>()
             .ForMember(
                 dest => dest.NameTeatcher,
                 opt => opt.MapFrom(src =>
                     src.Teacher.FirstName + " " + src.Teacher.LastName)
             );
            CreateMap<CreateSubjectDTO, Subject>()
              .ForMember(
                  dest => dest.TeacherId,
                  opt => opt.MapFrom(src => src.TeatcherID)
              );

            CreateMap<UpdateSubjectsDTO, Subject>()
                .ForMember(
                    dest => dest.TeacherId,
                    opt => opt.MapFrom(src => src.TeatcherID)
                );
        }
    }
}
