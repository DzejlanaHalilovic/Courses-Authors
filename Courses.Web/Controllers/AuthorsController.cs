using AutoMapper;
using Courses.Data.Models;
using Courses.Infrastructure;
using Courses.Web.DTOs;
using Courses.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses.Web.Controllers
{
    [ApiController]
    //bazna ruta
    // I domaci: Kreirati CRUD za Kurseve (zajedno sa DTO)
    // II domaci: Upotrebite servis koji smo kreirali na prethodnim vezbama
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
      
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public AuthorsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var lista = (await unitOfWork.AuthorsRepository.GetAll()).AsQueryable();
            //lista.ApplySorting(new Author);
            //lista.ApplyPaging(....);
            var listb = mapper.Map<List<GetAuthorDTO>>(lista);
            return Ok(listb);

        }

        [HttpGet("{id}")]
        //[Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var author = await unitOfWork.AuthorsRepository.getById(id);
            if (author == null)
                return NotFound($"Author with id = {id} does not exist");
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(SaveAuthorDTO author)
        {
            //validacija na osnovu uslova(atributa) koje smo postavili, npr. maxLength, required....
            //Nick Chapsas
            if (!ModelState.IsValid)
            {
                return BadRequest(new {message = "Author is not valid"});
            }

            var newAuthor = mapper.Map<Author>(author);
            await unitOfWork.AuthorsRepository.Add(newAuthor);
            await unitOfWork.CompleteAsync();
            return Ok(new {message = " Added "});
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(int id, SaveAuthorDTO author)
        {
            var currentAuthor = await unitOfWork.AuthorsRepository.getById(id);
            if (currentAuthor == null)
                return NotFound(new {message = "Author is not found" });
            mapper.Map<SaveAuthorDTO, Author>(author, currentAuthor);
            await unitOfWork.CompleteAsync();
            return Ok(new {message = "Updated"});

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var currentAuthor = await unitOfWork.AuthorsRepository.getById(id);
            if (currentAuthor == null)
                return NotFound(new {message = "Author is not found" });
            await unitOfWork.AuthorsRepository.Remove(currentAuthor);
            await unitOfWork.CompleteAsync();
            return Ok(new {message = "Deleted"});
        }
    }



}
