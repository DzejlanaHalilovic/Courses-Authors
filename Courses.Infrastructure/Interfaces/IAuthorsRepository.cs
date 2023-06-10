using Courses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Infrastructure.Interfaces
{
    public interface IAuthorsRepository:IRepository<Author>
    {
        //void Add(Author author);
        //void Remove(Author author);
        //Author GetById(int id);
        //IEnumerable<Author> GetAll();


        Task<Author> GetBestAuthor();
    }
}
