using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.BlazorApp.Models;

namespace RMS.BlazorApp.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public InventoriesController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: api/Inventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventories()
        {
          if (_context.Inventories == null)
          {
              return NotFound();
          }
            return await _context.Inventories.ToListAsync();
        }

        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetInventory(int id)
        {
          if (_context.Inventories == null)
          {
              return NotFound();
          }
            var inventory = await _context.Inventories.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }

            return inventory;
        }

        [HttpGet("Item/{id}")]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetInventoriesByItem(int id)
        {
            if (_context.Inventories == null)
            {
                return NotFound();
            }
            var inventories = await _context.Inventories.Where(x => x.ItemId == id).ToListAsync();
            return inventories; 
        }

        [HttpGet("Item/Include")]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetPurchaseDetailsWithPurchaseOrder()
        {
            if (_context.Inventories == null)
            {
                return NotFound();
            }
            return await _context.Inventories.Include(x => x.ItemId).ToListAsync();
        }

        [HttpGet("Item/{id}/Include")]
        public async Task<ActionResult<Inventory>> GetInventoryWithItem(int id) 
        {
            if (_context.Inventories == null) 
            {
                return NotFound(); 
            }
            var inventories = await _context.Inventories.Include(x => x.ItemId).FirstOrDefaultAsync(x => x.InventoryId == id);

            if (inventories == null)
            {
                return NotFound();
            }
            return inventories;
        }

        // PUT: api/Inventories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, Inventory inventory)
        {
            if (id != inventory.InventoryId)
            {
                return BadRequest();
            }

            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

        // POST: api/Inventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
          if (_context.Inventories == null)
          {
              return Problem("Entity set 'RestaurantDbContext.Inventories'  is null.");
          }
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventory", new { id = inventory.InventoryId }, inventory);
        }

        // DELETE: api/Inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            if (_context.Inventories == null)
            {
                return NotFound();
            }
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return (_context.Inventories?.Any(e => e.InventoryId == id)).GetValueOrDefault();
        }
    }
}
