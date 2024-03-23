using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class CoursesController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;


        #region GET
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _context.Courses.ToListAsync());


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course != null)
            {
                return Ok(course);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne(CourseRegistration course)
        {
            if (ModelState.IsValid)
            {
                var courseEntity = new CourseEntity
                {
                    Title = course.Title,
                    Price = course.Price,
                    DiscountPrice = course.DiscountPrice,
                    Hours = course.Hours,
                    IsBestSeller = course.IsBestSeller,
                    LikesInNumbers = course.LikesInNumbers,
                    LikesInPoints = course.LikesInPoints,
                    Author = course.Author,
                };

                _context.Courses.Add(courseEntity);
                await _context.SaveChangesAsync();


                return Created("", (CourseModel)courseEntity);
            }

            return BadRequest();
        }

        #endregion
    }
}
