using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class CategoriesController(DataContext dataContext) : ControllerBase
    {
        private readonly DataContext _dataContext = dataContext;


        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _dataContext.Categories.OrderBy(o => o.CategoryName).ToListAsync();
            return Ok(CategoryFactory.Create(categories));
        }
    }
}
