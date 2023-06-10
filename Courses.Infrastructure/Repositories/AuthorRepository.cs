using Courses.Data.Models;
using Courses.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorsRepository
    {
        private readonly Data.AppContext context;
        public AuthorRepository(Data.AppContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Author> GetBestAuthor()
        {
            return await context.Authors.FirstOrDefaultAsync(x => x.Name.Contains("BEST"));
        }
    }
}
