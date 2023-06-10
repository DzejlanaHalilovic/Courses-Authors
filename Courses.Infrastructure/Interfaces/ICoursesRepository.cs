using Courses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.Interfaces
{
    public interface ICoursesRepository:IRepository<Course>
    {
        //void Add(Course course);
        //void Remove(Course course);
        //Course GetById(int id);
        //IEnumerable<Course> GetAll();


        //da li se ovde dodaju specificne metode za kurseve??
    }
}
