using System.ComponentModel.DataAnnotations;

namespace Courses.Web.DTOs
{
    public class SaveCourse
    {
       
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public int Level { get; set; }
        public float FullPrice { get; set; }
        public int AuthorId { get; set; }
    }
}
