using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Data.Models
{
    public class Course
    {
        public Course()
        {
            Tags = new HashSet<Tag>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [Required]
        public int Level { get; set; }
        public float FullPrice { get; set; }
        public Author Author { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public Cover Cover { get; set; }
    }

}
