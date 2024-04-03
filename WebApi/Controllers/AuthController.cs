using Infrastructure.Contexts;
using Infrastructure.Factory;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController(DataContext dataContext, IConfiguration configuration) : Controller
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly IConfiguration _configuration = configuration;


        [UseApiKey]
        [HttpPost]
        [Route("token")]
        public IActionResult GetToken(UserLoginForm form)
        {
            if (ModelState.IsValid)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        //new(ClaimTypes.Email, form.Email),
                        //new(ClaimTypes.Name, form.Email),
                        new(ClaimTypes.Name, form.GenerateToken)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(tokenString);
            }

            return Unauthorized();
        }



        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> Register(UserRegistrationForm form)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (!await _dataContext.Users.AnyAsync(x => x.Email == form.Email))
        //        {
        //            _dataContext.Users.Add(UserFactory.Create(form));
        //            await _dataContext.SaveChangesAsync();
        //            return Created("", null);
        //        }

        //        return Conflict();
        //    }

        //    return BadRequest();
        //}

        //[HttpPost]
        //[Route("login")]

        //public async Task<IActionResult> Login(UserLoginForm form)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var userEntity = await _dataContext.Users.FirstOrDefaultAsync(x => x.Email == form.Email);
        //        if (userEntity != null && PasswordHasher.ValidateSecurePassword(form.Password, userEntity.Password))
        //        {
        //            var tokenHandler = new JwtSecurityTokenHandler();
        //            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!);
        //            var tokenDescriptor = new SecurityTokenDescriptor
        //            {
        //                Subject = new ClaimsIdentity(new Claim[]
        //                {
        //                    new(ClaimTypes.NameIdentifier, userEntity.Id.ToString()),
        //                    new(ClaimTypes.Email, userEntity.Email),
        //                    new(ClaimTypes.Name, userEntity.Email),
        //                    new(ClaimTypes.Role, "User")
        //                }),
        //                Expires = DateTime.UtcNow.AddDays(7),
        //                Issuer = _configuration["Jwt:Secret"],
        //                Audience = _configuration["Jwt:Audience"],
        //                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //            };

        //            var token = tokenHandler.CreateToken(tokenDescriptor);
        //            var tokenString = tokenHandler.WriteToken(token);

        //            return Ok(new { tokenString });
        //        }
        //    }

        //    return Unauthorized();
        //}
    }
}
