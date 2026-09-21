using AutoMapper;
using School.Models;
using SchoolProjcet.DTOs.ClassRoomDTOs;
namespace SchoolProjcet.Mapper.ClassRoomMapping
{
    public class ClassRoomProfail:Profile
    {
        public ClassRoomProfail()
        {
            CreateMap<ClassRoom,ClassRoomDTO>().ReverseMap();
            CreateMap<CreateClassRoomDTO,ClassRoom>().ReverseMap();
            CreateMap<UpdateClassRoomDTO,ClassRoom>().ReverseMap();
        }
    }
}
