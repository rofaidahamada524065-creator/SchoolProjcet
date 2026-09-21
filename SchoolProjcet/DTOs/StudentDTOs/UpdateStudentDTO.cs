namespace SchoolProjcet.DTOs.StudentDTOs
{
    public class UpdateStudentDTO
    {
        public int ClassRoomId { get; set; }
        public string FirstName { get; set; }


        public string LastName { get; set; }

        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
