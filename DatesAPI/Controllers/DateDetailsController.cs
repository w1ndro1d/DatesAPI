using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatesAPI.Models;

namespace DatesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateDetailsController : ControllerBase
    {
        private readonly DateDetailsContext _context;
        private readonly UserDetailsContext _userDetailsContext;

        public DateDetailsController(DateDetailsContext context, UserDetailsContext userDetailsContext)
        {
            _context = context;
            _userDetailsContext = userDetailsContext;
        }
        
        //READ
        // GET: api/DateDetails
        [HttpGet("{userEmail}")]
        public async Task<ActionResult<IEnumerable<DateDetails>>> GetDateDetailsForUser(string userEmail)
        {
            //get userID from user email
            int userID = await _userDetailsContext.UserDetails.Where(d => d.Email == userEmail).Select(d => d.UserID).FirstOrDefaultAsync();
            //for user.email in error message
            var user = await _userDetailsContext.UserDetails.FirstOrDefaultAsync(u => u.UserID == userID);
            if(user == null)
            {
                return NotFound("User not found!");
            }
            
            //fetch dates that match the userID
            var userDateDetails = await _context.DateDetails.Where(d => d.UserId == userID).ToListAsync();
            if(userDateDetails == null || userDateDetails.Count ==0)
            {
                return NotFound($"No dates found for {user.Email}!");
            }
            return Ok(userDateDetails);
        }

        ////READ WITH ID
        //// GET: api/DateDetails/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<DateDetails>> GetDateDetails(int id)
        //{
        //  if (_context.DateDetails == null)
        //  {
        //      return NotFound();
        //  }
        //    var dateDetails = await _context.DateDetails.FindAsync(id);

        //    if (dateDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    return dateDetails;
        //}

        //UPDATE WITH ID
        // PUT: api/DateDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDateDetails(int id, DateDetails dateDetails)
        {
            if (id != dateDetails.DateId)
            {
                return BadRequest();
            }

            _context.Entry(dateDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DateDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
            return Ok(await _context.DateDetails.ToListAsync());
        }

        //CREATE
        // POST: api/DateDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DateDetails>> PostDateDetails(DateDetails dateDetails)
        {
          if (_context.DateDetails == null)
          {
              return Problem("Entity set 'DateDetailsContext.DateDetails'  is null.");
          }
            _context.DateDetails.Add(dateDetails);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetDateDetails", new { id = dateDetails.DateId }, dateDetails);
            return Ok(await _context.DateDetails.ToListAsync());
        }

        //DELETE
        // DELETE: api/DateDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDateDetails(int id)
        {
            if (_context.DateDetails == null)
            {
                return NotFound();
            }
            var dateDetails = await _context.DateDetails.FindAsync(id);
            if (dateDetails == null)
            {
                return NotFound();
            }

            _context.DateDetails.Remove(dateDetails);
            await _context.SaveChangesAsync();

            //return NoContent();
            return Ok(await _context.DateDetails.ToListAsync());
        }

        private bool DateDetailsExists(int id)
        {
            return (_context.DateDetails?.Any(e => e.DateId == id)).GetValueOrDefault();
        }
    }
}
