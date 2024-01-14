using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.BlazorApp.Models;

namespace RMS.BlazorApp.Server.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitionsController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public RequisitionsController(RestaurantDbContext context) 
        {
            _context = context;
        }

        // GET: api/Requisitions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Requisition>>> GetRequisitions()
        {
            if (_context.Requisitions == null)
            {
                return NotFound();
            }
            return await _context.Requisitions.ToListAsync();
        }

        // GET: api/Requisitions/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Requisition>> GetRequisition(int id)
        {
            if (_context.Requisitions == null)
            {
                return NotFound();
            }
            var requisition = await _context.Requisitions.FindAsync(id);

            if (requisition == null)
            {
                return NotFound();
            }

            return requisition;
        }

        // PUT: api/Requisitions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequisition(int id, Requisition requisition)
        {
            if (id != requisition.RequisitionId)
            {
                return BadRequest();
            }

            _context.Entry(requisition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequisitionExists(id))
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

        // POST: api/Requisitions 
        [HttpPost]
        public async Task<ActionResult<Requisition>> PostRequisition(Requisition requisition)
        {
            if (_context.Requisitions == null)
            {
                return Problem("Entity set 'RestaurantDbContext.Requisitions'  is null.");
            }
            _context.Requisitions.Add(requisition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequisition", new { id = requisition.RequisitionId }, requisition);
        }

        // DELETE: api/Requisitions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequisition(int id)
        {
            if (_context.Requisitions == null)
            {
                return NotFound();
            }
            var requisition = await _context.Requisitions.FindAsync(id);
            if (requisition == null)
            {
                return NotFound();
            }

            _context.Requisitions.Remove(requisition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequisitionExists(int id)
        {
            return (_context.Requisitions?.Any(e => e.RequisitionId == id)).GetValueOrDefault();
        }
    }
}
