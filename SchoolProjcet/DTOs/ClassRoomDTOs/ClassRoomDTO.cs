using System.ComponentModel.DataAnnotations;

namespace SchoolProjcet.DTOs.ClassRoomDTOs
{
    public class ClassRoomDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

       
        public int GradeLevel { get; set; }

       
        public int Capacity { get; set; }
    }
}
