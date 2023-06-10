using Courses.Data.Models;
using Courses.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.Repositories
{
    public class CoursesRepository : Repository<Course>, ICoursesRepository
    {
        private readonly Data.AppContext _appContext;
        public CoursesRepository(Data.AppContext context) : base(context)
        {
            _appContext = context;
        }
    }
}
