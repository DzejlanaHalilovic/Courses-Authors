using System.ComponentModel.DataAnnotations;

namespace Courses.Web.DTOs
{
    public class SaveAuthorDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

    }


}
