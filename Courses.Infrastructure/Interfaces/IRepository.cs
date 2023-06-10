using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.Interfaces
{
   public interface IRepository<T> where T:class
    {
        Task<bool> Add(T entity);
        Task<bool> Remove(T entity);
        Task<T> getById(int id);

       Task<List<T>> GetAll();
    }
}
