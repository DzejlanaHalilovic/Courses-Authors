using System.ComponentModel.DataAnnotations.Schema;

namespace Courses.Data.Models
{
    public class Cover
    {
        public int Id { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }

}
