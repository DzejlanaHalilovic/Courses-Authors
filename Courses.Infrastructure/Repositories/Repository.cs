using Courses.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly Data.AppContext context;
        public Repository(Data.AppContext context)
        {
            this.context = context;

        }
        
        public async Task<bool> Add(T entity)
        {
           context.Set<T>().Add(entity);
           var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> getById(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Remove(T entity)
        {
            context.Set<T>().Remove(entity);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<T>> GetFilteredData(Expression<Func<T, bool>> predicate) 
        {
            
            return await context.Set<T>().Where(predicate).ToListAsync();
        }
    }

    }

