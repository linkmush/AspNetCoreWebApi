using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
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

        #endregion

        #region CREATE

        [Authorize]               // UPDATE och Delete ska också skyddas med Accesstoken. 
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
                    ImageUrl = course.ImageUrl
                };

                _context.Courses.Add(courseEntity);
                await _context.SaveChangesAsync();


                return Created("", (CourseModel)courseEntity);
            }

            return BadRequest();
        }

        #endregion

        #region Update

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseUpdateModel course)
        {
            var courseEntity = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (courseEntity != null)
            {
                courseEntity.Title = course.Title;
                courseEntity.Price = course.Price;
                courseEntity.DiscountPrice = course.DiscountPrice;
                courseEntity.Hours = course.Hours;
                courseEntity.IsBestSeller = course.IsBestSeller;
                courseEntity.LikesInNumbers = course.LikesInNumbers;
                courseEntity.LikesInPoints = course.LikesInPoints;
                courseEntity.Author = course.Author;
                courseEntity.ImageUrl = course.ImageUrl;

                _context.Courses.Update(courseEntity);
                await _context.SaveChangesAsync();

                return Ok(courseEntity);
            }

            return NotFound();
        }

        #endregion

        #region DELETE

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return NotFound();

        }

        #endregion
    }
}
