using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UseApiKey]
    public class ContactsController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpPost]
        public async Task<IActionResult> Create(ContactDto dto)
        {
            if (ModelState.IsValid)
            {

                if (!await _context.Contacts.AnyAsync(x => x.Email == dto.Email))
                {
                    var contactEntity = ContactFactory.Create(dto);
                    _context.Contacts.Add(contactEntity);
                    await _context.SaveChangesAsync();
                    return Created("", null);
                }
                else
                {
                    return Conflict();
                }
            }
            return BadRequest();
        }
    }
}
