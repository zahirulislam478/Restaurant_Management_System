using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.BlazorApp.Models;

namespace RMS.BlazorApp.Server.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyMenusController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public DailyMenusController(RestaurantDbContext context) 
        {
            _context = context;
        }

        // GET: api/DailyMenus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyMenu>>> GetDailyMenus()
        {
            if (_context.DailyMenus == null)
            {
                return NotFound();
            }
            return await _context.DailyMenus.ToListAsync();
        }

        // GET: api/DailyMenus/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<DailyMenu>> GetDailyMenu(int id)
        {
            if (_context.DailyMenus == null)
            {
                return NotFound();
            }
            var dailyMenu = await _context.DailyMenus.FindAsync(id);

            if (dailyMenu == null)
            {
                return NotFound();
            }

            return dailyMenu;
        }

        [HttpGet("Requisitions/Include")]
        public async Task<ActionResult<IEnumerable<DailyMenu>>> GetDailyMenusWithRequisitions()
        { 
            if (_context.DailyMenus == null)
            {
                return NotFound(); 
            }
            return await _context.DailyMenus.Include(x => x.Requisitions).ToListAsync();
        }

        [HttpGet("Requisitions/{id}/Include")]
        public async Task<ActionResult<DailyMenu>> GetDailyMenusWithRequisitions(int id)
        {
            if (_context.DailyMenus == null) 
            {
                return NotFound();
            }

            var dailyMenu = await _context.DailyMenus.Include(x => x.Requisitions).FirstOrDefaultAsync(x => x.DailyMenuId == id);

            if (dailyMenu == null)
            {
                return NotFound();
            }
            return dailyMenu;
        }

        // PUT: api/DailyMenus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDailyMenu(int id, DailyMenu dailyMenu)
        {
            if (id != dailyMenu.DailyMenuId)
            {
                return BadRequest();
            }

            _context.Entry(dailyMenu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyMenuExists(id))
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

        // POST: api/DailyMenus 
        [HttpPost]
        public async Task<ActionResult<DailyMenu>> PostDailyMenu(DailyMenu dailyMenu)
        {
            if (_context.DailyMenus == null)
            {
                return Problem("Entity set 'RestaurantDbContext.DailyMenus'  is null.");
            }
            _context.DailyMenus.Add(dailyMenu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDailyMenu", new { id = dailyMenu.DailyMenuId }, dailyMenu);
        }

        // DELETE: api/DailyMenus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyMenu(int id)
        {
            if (_context.DailyMenus == null)
            {
                return NotFound();
            }
            var dailyMenu = await _context.DailyMenus.FindAsync(id);
            if (dailyMenu == null)
            {
                return NotFound();
            }

            _context.DailyMenus.Remove(dailyMenu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DailyMenuExists(int id)
        {
            return (_context.DailyMenus?.Any(e => e.DailyMenuId == id)).GetValueOrDefault();
        }
    }
}
