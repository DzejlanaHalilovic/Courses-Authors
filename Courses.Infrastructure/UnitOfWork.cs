using Courses.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Data.AppContext context;
        public UnitOfWork(Data.AppContext context, IAuthorsRepository authorsRepository, ICoursesRepository coursesRepository)
        {
            this.context = context;
            CoursesRepository = coursesRepository;
            AuthorsRepository = authorsRepository;
        }

        public IAuthorsRepository AuthorsRepository { get; }

        public ICoursesRepository CoursesRepository { get; }

        public async Task<bool> CompleteAsync()
        {
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
    }
}
