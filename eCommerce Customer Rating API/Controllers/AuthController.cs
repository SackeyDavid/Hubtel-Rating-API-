using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce_Customer_Rating_API.Models;

namespace eCommerce_Customer_Rating_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HubtelApplicationDbContext _context;

        public AuthController(HubtelApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Auth
        //[HttpGet]
        private async Task<ActionResult<IEnumerable<Auth>>> GetAuth()
        {
            return await _context!.Auth!.ToListAsync();
        }

        // GET: api/Auth/5
        //[HttpGet("{id}")]
        private async Task<ActionResult<Auth>> GetAuth(int id)
        {
            var auth = await _context!.Auth!.FindAsync(id);

            if (auth == null)
            {
                return NotFound();
            }

            return auth;
        }

        // PUT: api/Auth/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        private async Task<IActionResult> PutAuth(int id, Auth auth)
        {
            if (id != auth.UserId)
            {
                return BadRequest();
            }

            _context.Entry(auth).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //[Route("~/Auth")]
        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        private async Task<ActionResult<Auth>> PostAuth(Auth auth)
        {
            _context!.Auth!.Add(auth);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuth", new { id = auth.UserId }, auth);
        }

        // DELETE: api/Auth/5
        //[HttpDelete("{id}")]
        private async Task<IActionResult> DeleteAuth(int id)
        {
            var auth = await _context!.Auth!.FindAsync(id);
            if (auth == null)
            {
                return NotFound();
            }

            _context.Auth.Remove(auth);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthExists(int id)
        {
            return _context!.Auth!.Any(e => e.UserId == id);
        }
    }
}
