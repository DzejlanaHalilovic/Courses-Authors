using Courses.Contract.Models;

namespace Courses.Web.DTOs
{
    // ono implementrianje razdvajenej u page i sort imamo dva interfejsa ,
    // i jos jer ima mogucnost nasledjivanje visestruko u interfaces
    public class CourseQueryDTO : IQueryObj
    {
        public int? AuthorId { get; set; }
        public string? Name { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAsc { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get ; set ; }
    }
}
