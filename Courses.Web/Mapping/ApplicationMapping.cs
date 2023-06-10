using AutoMapper;
using Courses.Contract.Models;
using Courses.Data.Models;
using Courses.Web.DTOs;

namespace Courses.Web.Mapping
{
    public class ApplicationMapping : Profile
    {
        public ApplicationMapping()
        {
            //primer from models to DTOs
            CreateMap<Author, GetAuthorDTO>();
            CreateMap<SaveAuthorDTO, Author>();
            //preslikava se Author u GetAuthorDTO
            //.ForMember(d => d.Name, opt => opt.MapFrom(s => s.Ime));
            //ovo znaci da Ime neke klase preslikavamo u Name neke druge klase
            //u modelu se zove IME, a u dtos NAME on ne zna razliku u tom slucaju kazemo formember


            //ForMember() metoda se koristi kada se imena polja u izvornom objektu i ciljnom objektu ne podudaraju.
            //U tom slučaju, morate ručno da konfigurišete AutoMapper kako bi se ta polja ispravno mapirala.
            //primeri from DTOs to Models
            CreateMap<SaveCourse, Course>();
            //dodato:
            CreateMap<Course, GetCourse>();

            CreateMap<UpdateCourse, Course>();

           CreateMap(typeof(QueryResult<>), typeof(QueryResultDTO<>));
        }
    }
}
