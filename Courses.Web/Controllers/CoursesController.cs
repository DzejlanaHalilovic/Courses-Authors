using Courses.Data.Models;
using Courses.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using Courses.Data;
using System.Net.WebSockets;
using Courses.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoMapper;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Reflection.Metadata.Ecma335;
using System.Linq.Expressions;
using Courses.Web.Extensions;
using System.Collections;

namespace Courses.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        //private readonly Courses.Data.ICoursesServices coursesServices;
        //private readonly Courses.Data.AppContext appContext;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CoursesController> logger;
        private readonly ISender sender;
        private readonly IMapper mapper;
        


        public CoursesController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CoursesController> logger,
            ISender sender
            )
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery]CourseQueryDTO courseQuery)
        {
            logger.LogInformation("Called get all courses method");
            // a pre je bilo var list = repository .GetAll(); tako je sve bilo gde je sad ovo nitofwork.coursesrepository
            var query = (await unitOfWork.CoursesRepository.GetAll()).AsQueryable();
            logger.LogInformation($"Num of courses from db ={query.Count()}");
            if (courseQuery.AuthorId.HasValue)
            {
                //filtriranje po autoru
                logger.LogInformation($"Courses have been filtered by authodId = {courseQuery.AuthorId}");
                query = query.Where(c => c.AuthorId == courseQuery.AuthorId);
            }
            //filtriranje po imenu
            if(!string.IsNullOrEmpty(courseQuery.Name))
            {
                query = query.Where(c => c.Name.Contains(courseQuery.Name));
            }
           //key value pair, kljuc i vrednost, kod nas kljuc ime po kojoj hocemo da sortiramo, ne dozvoljavamo po vise, samo po jednoj, i na osnovu toga imamo exxpresion, odnsosno linq izraz
            var sortColumns = new Dictionary<string, Expression<Func<Course, object>>>
            {
                ["Namme"] = c => c.Name,
                ["FullPrice"] = c => c.FullPrice,
                ["AuthorId"] = c => c.AuthorId
            };
            logger.LogInformation($"applied sorting");
            query = query.ApplySorting(courseQuery, sortColumns);

            logger.LogInformation($"applied paging");
            query = query.ApplyPaging(courseQuery);
            //var sortAuthorColumns = new Dictionary<string, Expression<Func<Author, object>>>
            //{
            //    ["FirstName"] = c => c.Name,
            //    ["LastName"] = c => c.FullPrice,
            //    ["BirthDate"] = c => c.AuthorId
            //};

            var listToShow = mapper.Map<IEnumerable<GetCourse>>(query.ToList());
            QueryResultDTO<GetCourse> result = new QueryResultDTO<GetCourse>();
            result.Total = listToShow.Count();
            result.Data = listToShow;
            logger.LogInformation($"num of courses to return= {listToShow.Count()}");
            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneCourse(int id) 
        {
            var course = await unitOfWork.CoursesRepository.getById(id);
            var courseToShow = mapper.Map<GetCourse>(course);
            if(courseToShow == null)
            {
                return NotFound($"there is no course with id {id}");

            }
            return Ok(courseToShow);
        }

        [HttpPost]
        public async Task<IActionResult> PostCourse(SaveCourse course) {

            logger.LogInformation("Start adding new course");
            if(!ModelState.IsValid) 
            {
                logger.LogError("Course is not valid");
                return BadRequest(ModelState);
            }
            var Course = mapper.Map<Course>(course);
            await unitOfWork.CoursesRepository.Add(Course);
            var msg = new
            {
                message = "Added"
            };
            logger.LogInformation("add course");

            ISender e = new EmailSender();
           await  e.Send("Course added succesfully", "edina@gmail.com");
            return Ok(msg);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id) 
        {
            var course = await unitOfWork.CoursesRepository.getById(id);
            if (course == null)
                return NotFound();
            await unitOfWork.CoursesRepository.Remove(course);
            await unitOfWork.CompleteAsync();
            var msg = new
            {
                message = "Deleted"
            };

            return Ok(msg);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse(int id, UpdateCourse course)
        {
            var curCourse = await unitOfWork.CoursesRepository.getById(id);
            mapper.Map<UpdateCourse,Course>(course, curCourse);
            await unitOfWork.CompleteAsync();
            var msg = new

            {
                message = "updated"
            };
            return Ok(msg);
        }

    }
    public interface ISender
    {
         Task Send(string message, string to);
    }
    public class EmailSender : ISender
    {
        public async Task Send(string message, string to)
        {
            //HttpClient http = new HttpClient();
            //http.BaseAddress = new Uri(@"http://smtp.gmail.com");
            //HttpContent content = new HttpContent();
            //http.PostAsync("sendEmail", content);
            //if (result.Iscomplited)
            //    Console.log("Email send");
        }
    }
    //public class SmsSender : ISender
    //{
    //    public Task Send(string message, string to)
    //    //{
    //    //    Twilio tw = new Twilio();
    //    //    tw.SendSms(message, to);
    //    //    if (Twilio.CheckDelivery(id))
    //    //        Console.Write("sms deliver succcesfyll");
    //    }
    //}
    public abstract class Oblik
    {
        public abstract int Povrsina();

    }
    public class Kvadrat : Oblik
    {
        public int Stranica { get; set; }

        public override int Povrsina()
        {
            return Stranica * Stranica;
        }
    }
    public class Pravougaonik : Oblik
    {
        public int A { get; set; }
        public int B { get; set; }

        public override int Povrsina()
        {
            return A * B;
        }
    }
    public class Krug : Oblik
    {
        public int r { get; set; }
        public override int Povrsina()
        {
            return (int)(r * r * 3.14);
        }
    }

    public class KalkulatorPovrsine
    {
        public int Suma(List<Oblik> list)
        {
            int s = 0;
            foreach(var item in list)
            {
                s += item.Povrsina();
            }
            return s;
        }
        
    }
    //public class Program
    //{
    //    public Program()
    //    {
    //        ISender sender = new SmsSender();
    //        var c = new CoursesController();
    // treba u program da stavi sender, isender preko chapper
    //    }
    //}



}

// domaci da odradite ostala mappiranja ,i da radi asinhrono 
// kontrollere i repozitorijum da bude asinhroni