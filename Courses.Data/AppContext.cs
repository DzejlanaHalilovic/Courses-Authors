using Courses.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Data
{
    public class AppContext : IdentityDbContext<ApplicationUser>
        
    {
        public AppContext()
        {
        }

        public AppContext(DbContextOptions<AppContext> options): base(options) { }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Cover> Covers { get; set; }
        public DbSet<Tag> Tags { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new List<Author>
                {
                    new Author {Id =1 , Name ="1st Author"},
                    new Author {Id=2,   Name="2st Author"},
                    new Author {Id=3, Name="3st Author"}
                }
                );
            modelBuilder.Entity<Course>().HasData(
                new List<Course>
                {
                    new Course {Id=1, Name="c# Basics", AuthorId=1,Level=1,Description="Some desc",FullPrice=33.66f}
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
