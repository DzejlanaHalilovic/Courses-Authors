using Courses.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure
{
    public interface IUnitOfWork
    {
        IAuthorsRepository AuthorsRepository { get; }
        ICoursesRepository CoursesRepository { get; }

        Task<bool> CompleteAsync();
    }
}
