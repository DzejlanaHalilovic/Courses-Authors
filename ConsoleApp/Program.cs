// See https://aka.ms/new-console-template for more information
using Courses.Data;

Console.WriteLine("Hello, World!");

CoursesServices c = new CoursesServices();
var courses = c.GetCourses();
foreach(var course in courses)
{
    Console.WriteLine($"{course.Id} {course.Name}");
}

var authors = c.GetAuthors();
foreach(var author in authors)
{
    Console.WriteLine($"{author.Id} {author.Name}");
}
var price19 = c.GetCoursesLower19();
foreach(var prices in price19)
{
    Console.WriteLine($"{prices.Id} {prices.Name}");
}
var course5 = c.GetCourse(5);
if(course5 == null)
{
    Console.WriteLine("neispravan id");
}

var courseId1 = c.GetCourse(1,true);


if(courseId1 == null)
{
    Console.WriteLine("neispravan id");
}
else
{
    Console.WriteLine($"Kurs {courseId1.Name} je kreirao autor {courseId1.Author.Name}");
}

//sp sporprostizen 


var cl1 = c.GetLevel1AndOrderByIdDesc();
foreach(var item in cl1)
{
    Console.WriteLine(item.Name);
}



