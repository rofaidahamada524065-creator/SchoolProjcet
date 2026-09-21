using System.ComponentModel.DataAnnotations;

namespace SchoolProjcet.DTOs.SubjectDTOs
{
    public class SubjectDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
    
        public string? Description { get; set; }
       
        public int MaxGrade { get; set; }

        public string NameTeatcher {  get; set; }

     
    }
}
