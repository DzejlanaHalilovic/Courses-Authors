using Courses.Data.Models;

namespace Courses.Web.DTOs
{
    public class GetAuthorWithCoursesDTO
    {
        public GetAuthorWithCoursesDTO()
        {
            Courses = new HashSet<Course>();
        }
        public int Id { get; set; }
        public  string Name { get; set; }
        public ICollection<Course> Courses { get;set; }

    }


}
