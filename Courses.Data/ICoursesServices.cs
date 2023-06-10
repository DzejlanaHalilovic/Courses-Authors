using Courses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Data
{
    public interface ICoursesServices
    {
        public List<Course> GetCourses();
        public List<Author> GetAuthors();
        public List<Course> GetCoursesLower19();
        public Course? GetCourse(int id, bool includeRelatedAuthor = false);
        public List<Course> GetLevel1AndOrderByIdDesc();
        public bool CreateCourse (Course course);
        public bool DeleteCourse (int id );
        public bool UpdateCourse (int id, Course course);

    }
}
