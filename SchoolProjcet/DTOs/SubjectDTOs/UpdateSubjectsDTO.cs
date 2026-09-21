namespace SchoolProjcet.DTOs.SubjectDTOs
{
    public class UpdateSubjectsDTO
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int MaxGrade { get; set; }
        public int TeatcherID { get; set; }
    }
}
