﻿using System.ComponentModel.DataAnnotations;

namespace Courses.Data.Models
{
    public class Author
    {
        public Author()
        {
            Courses = new HashSet<Course>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
    }

}
