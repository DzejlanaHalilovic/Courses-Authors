using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Contract.Models
{
    //da vismo sacuvali nasu aplikaciju da uvek ima ista svojstva za sortitanje, filtriranje i paginaciju
    public interface IQueryObj
    {
        //sortiranje
        public string SortBy { get; set; }
        public bool IsSortAsc { get; set; }
        //paginacija
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
