using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce_Customer_Rating_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace eCommerce_Customer_Rating_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RatingsController : ControllerBase
    {
        private readonly HubtelApplicationDbContext _context;

        public RatingsController(HubtelApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ratings>>> GetRatings()
        {
            return await _context!.Ratings!.ToListAsync();
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ratings>> GetRatings(int id)
        {
            var ratings = await _context!.Ratings!.FindAsync(id);

            if (ratings == null)
            {
                return NotFound();
            }

            return ratings;
        }

        // PUT: api/Ratings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRatings(int id, Ratings ratings)
        {
            if (id != ratings.RatingsId)
            {
                return BadRequest();
            }

            _context.Entry(ratings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingsExists(id))
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

        // POST: api/Ratings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ratings>> PostRatings(Ratings ratings)
        {
            _context!.Ratings!.Add(ratings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRatings", new { id = ratings.RatingsId }, ratings);
        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRatings(int id)
        {
            var ratings = await _context!.Ratings!.FindAsync(id);
            if (ratings == null)
            {
                return NotFound();
            }

            _context.Ratings.Remove(ratings);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingsExists(int id)
        {
            return _context.Ratings!.Any(e => e.RatingsId == id);
        }
    }
}
