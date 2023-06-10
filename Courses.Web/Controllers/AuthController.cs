using Courses.Data.Models;
using Courses.Web.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Courses.Web.Controllers
{
    public class AuthController : Controller
    {
      

            //registarcija novog korinsika (kredencijale)
            // logovanje postojeceg korisnika(kredencijale)
            // 
            private readonly UserManager<ApplicationUser> _userManager;


            public AuthController(UserManager<ApplicationUser> userManager)
            {
                this._userManager = userManager;
            }
            [HttpPost]
            [Route("register")]
            public async Task<IActionResult> Register([FromBody] LoginModel model)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                try
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = model.UserName,
                        Email = model.UserName,

                    };

                    //createcejcing zvara identiti result
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)

                        return Ok(user);
                    return BadRequest(result.Errors.FirstOrDefault());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


            [HttpPost]
            [Route("login")]
            public async Task<IActionResult> Login([FromBody] LoginModel model)
            {
                if (!ModelState.IsValid)

                    return BadRequest(ModelState);

                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                    return BadRequest();

                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("78fUjkyzfLz56gTq"));
                    var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,model.UserName),
                //new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString())
            };

                    var token = new JwtSecurityToken(
                        expires: DateTime.Now.AddHours(1),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                        );
                    var toReturn = new JwtSecurityTokenHandler().WriteToken(token);
                    var obj = new
                    {
                        expires = DateTime.Now.AddHours(1),
                        token = toReturn

                    };
                    return Ok(obj);
                }
                else
                {
                    return BadRequest("Username and password not match");

                }
            }


        }
    }

