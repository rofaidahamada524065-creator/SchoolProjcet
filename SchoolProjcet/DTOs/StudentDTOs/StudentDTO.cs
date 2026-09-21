using System.ComponentModel.DataAnnotations;

namespace SchoolProjcet.DTOs.StudentDTOs
{
    public class StudentDTO
    {
        
        public int Id { get; set; }

        public string ClassRoomName { get; set; }
       public string FullName { get; set; }

        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
