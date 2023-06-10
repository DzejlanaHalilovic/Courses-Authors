namespace Courses.Web.DTOs
{
    public class QueryResultDTO<T> where T : class
    {
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

}
