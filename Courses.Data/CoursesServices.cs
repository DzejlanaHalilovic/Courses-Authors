using Courses.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Data
{ 
   public class CoursesServices : ICoursesServices
    {
         private readonly AppContext appContext;

        public CoursesServices(AppContext context)
        {
            this.appContext = context;
        }
        public List<Course> GetCourses()
        {
            return appContext.Courses.ToList();
//////            //var c = from course in appContext.Courses
//////            //        select course;
//////            //return c.ToList();
// filtriranje, sortiranje se koristi kod httpget requesta, na primer getall..

        }
        public bool CreateCourse(Course course)
        {
            if(appContext.Authors.Find(course.AuthorId) != null)
            {
                var answer = appContext.Courses.Add(course);
                appContext.SaveChanges();
                return answer != null;
            }
            return false;
           

        }

        public List<Author> GetAuthors()
        {
            return appContext.Authors.ToList();
        }
        public List<Course> GetCoursesLower19()
        {
            return appContext.Courses.Where(c => c.FullPrice < 19).ToList();
        }
       public Course? GetCourse(int id, bool includeRelatedAuthor = false)    
       {
            if(!includeRelatedAuthor)
               return appContext.Courses.Find(id);
          return appContext.Courses.Include(c=>c.Author).FirstOrDefault(c => c.Id == id);


//////            //return appContext.Courses.FirstOrDefault(c=>c.Id == id);


       }
//////        //eager loading cemo mi koristiti 
        public List<Course> GetLevel1AndOrderByIdDesc()/*)*/
        {
//////            //var courses = appContext.Courses;
         var courses = appContext.Courses.Include(c=>c.Author).Include(c=>c.Tags);
                
          var level1 = courses.Where(c => c.Level == 1);
          var sorted = level1.OrderByDescending(l => l.Id);
            var result = sorted.ToList();
//////           // var r1 = sorted.Where(s => s.Author.Name.Contains("Ensar"));

           // select * from courses where level= 1 order by Id desc;
        return result;

     }

        public bool DeleteCourse(int id)
        {
            var course = appContext.Courses.Find(id);
            if(course == null)
                return false;
            appContext.Courses.Remove(course);
            appContext.SaveChanges();
            return true;
        }

        public bool UpdateCourse(int id, Course course)
        {
            var currentCourse = appContext.Courses.Find(id);
            if(currentCourse == null)
                return false;
            currentCourse.Name = course.Name;
            currentCourse.Description = course.Description;
            currentCourse.FullPrice= course.FullPrice;
            currentCourse.Level = course.Level;
            appContext.SaveChanges();
            return true;

        }
    }
}
