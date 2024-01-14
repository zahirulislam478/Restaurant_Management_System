using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.BlazorApp.Models;

namespace RMS.BlazorApp.Server.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceivedDetailsController : ControllerBase
    {
        private readonly RestaurantDbContext _context; 

        public ReceivedDetailsController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: api/ReceivedDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReceivedDetail>>> GetReceivedDetails()
        {
            if (_context.ReceivedDetails == null)
            {
                return NotFound();
            }
            return await _context.ReceivedDetails.ToListAsync();
        }

        // GET: api/ReceivedDetails/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<ReceivedDetail>> GetReceivedDetail(int id)
        {
            if (_context.ReceivedDetails == null)
            {
                return NotFound();
            }
            var receivedDetail = await _context.ReceivedDetails.FindAsync(id);

            if (receivedDetail == null)
            {
                return NotFound();
            }

            return receivedDetail;
        }

        // PUT: api/ReceivedDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceivedDetail(int id, ReceivedDetail receivedDetail)
        {
            if (id != receivedDetail.ReceivedDetailId)
            {
                return BadRequest();
            }

            _context.Entry(receivedDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReceivedDetailExists(id))
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

        // POST: api/ReceivedDetails 
        [HttpPost]
        public async Task<ActionResult<ReceivedDetail>> PostReceivedDetail(ReceivedDetail receivedDetail)
        {
            if (_context.ReceivedDetails == null)
            {
                return Problem("Entity set 'RestaurantDbContext.ReceivedDetails'  is null.");
            }
            _context.ReceivedDetails.Add(receivedDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReceivedDetail", new { id = receivedDetail.ReceivedDetailId }, receivedDetail);
        }

        // DELETE: api/ReceivedDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceivedDetail(int id)
        {
            if (_context.ReceivedDetails == null)
            {
                return NotFound();
            }
            var receivedDetail = await _context.ReceivedDetails.FindAsync(id);
            if (receivedDetail == null)
            {
                return NotFound();
            }

            _context.ReceivedDetails.Remove(receivedDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReceivedDetailExists(int id)
        {
            return (_context.ReceivedDetails?.Any(e => e.ReceivedDetailId == id)).GetValueOrDefault();
        }
    }
}
