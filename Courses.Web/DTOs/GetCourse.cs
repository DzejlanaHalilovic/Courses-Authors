using Courses.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Courses.Web.DTOs
{
    public class GetCourse
    {
        
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public float FullPrice { get; set; }
        public Author Author { get; set; }
    }
}
