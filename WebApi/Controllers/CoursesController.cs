using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factory;
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
        public async Task<IActionResult> GetAll(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 10)
        {
            var query = _context.Courses.Include(i => i.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(category) && category != "all")
                query = query.Where(x => x.Category!.CategoryName == category);


            if (!string.IsNullOrEmpty(searchQuery))
                query = query.Where(x => x.Title.Contains(searchQuery) || x.Author!.Contains(searchQuery));

            query = query.OrderBy(o => o.LastUpdated);

            var courses = await query.ToListAsync();

            var response = new CourseResult
            {
                Succeeded = true,
                TotalItems = await query.CountAsync()
            };
            response.TotalPages = (int)Math.Ceiling(response.TotalItems / (double)pageSize);
            response.Courses = CourseFactory.Create(await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync());


            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var course = await _context.Courses.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (course != null)
            {
                return Ok(course);
            }

            return NotFound();
        }

        #endregion

        #region CREATE

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOne(CourseRegistration course)
        {
            if (ModelState.IsValid)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == course.CategoryName);

                if (category == null)
                {
                    category = new CategoryEntity { CategoryName = course.CategoryName };
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();
                }

                var courseEntity = new CourseEntity
                {
                    Title = course.Title,
                    Author = course.Author,
                    Price = course.Price,
                    DiscountPrice = course.DiscountPrice,
                    Hours = course.Hours,
                    LikesInNumbers = course.LikesInNumbers,
                    LikesInProcent = course.LikesInProcent,
                    IsDigital = course.IsDigital,
                    IsBestSeller = course.IsBestSeller,
                    ImageUrl = course.ImageUrl,
                    CategoryId = category.Id,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now
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
            var courseEntity = await _context.Courses.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (courseEntity != null)
            {
                var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == course.CategoryName);

                if (existingCategory == null)
                {
                    var newCategory = new CategoryEntity { CategoryName = course.CategoryName };
                    _context.Categories.Add(newCategory);
                    await _context.SaveChangesAsync();

                    courseEntity.CategoryId = newCategory.Id;
                }
                else
                {
                    courseEntity.CategoryId = existingCategory.Id;
                }

                courseEntity.Title = course.Title;
                courseEntity.Author = course.Author;
                courseEntity.Price = course.Price;
                courseEntity.DiscountPrice = course.DiscountPrice;
                courseEntity.Hours = course.Hours;
                courseEntity.LikesInNumbers = course.LikesInNumbers;
                courseEntity.LikesInProcent = course.LikesInProcent;
                courseEntity.IsDigital = course.IsDigital;
                courseEntity.IsBestSeller = course.IsBestSeller;
                courseEntity.ImageUrl = course.ImageUrl;
                courseEntity.LastUpdated = DateTime.Now;

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
