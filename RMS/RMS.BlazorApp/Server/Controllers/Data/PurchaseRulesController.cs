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
    public class PurchaseRulesController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public PurchaseRulesController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseRules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseRule>>> GetPurchaseRules()
        {
          if (_context.PurchaseRules == null)
          {
              return NotFound();
          }
            return await _context.PurchaseRules.ToListAsync();
        }

        // GET: api/PurchaseRules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseRule>> GetPurchaseRule(int id)
        {
          if (_context.PurchaseRules == null)
          {
              return NotFound();
          }
            var purchaseRule = await _context.PurchaseRules.FindAsync(id);

            if (purchaseRule == null)
            {
                return NotFound();
            }

            return purchaseRule;
        }

        // PUT: api/PurchaseRules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseRule(int id, PurchaseRule purchaseRule)
        {
            if (id != purchaseRule.PurchaseRuleId)
            {
                return BadRequest();
            }

            _context.Entry(purchaseRule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseRuleExists(id))
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

        // POST: api/PurchaseRules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseRule>> PostPurchaseRule(PurchaseRule purchaseRule)
        {
          if (_context.PurchaseRules == null)
          {
              return Problem("Entity set 'RestaurantDbContext.PurchaseRules'  is null.");
          }
            _context.PurchaseRules.Add(purchaseRule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseRule", new { id = purchaseRule.PurchaseRuleId }, purchaseRule);
        }

        // DELETE: api/PurchaseRules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseRule(int id)
        {
            if (_context.PurchaseRules == null)
            {
                return NotFound();
            }
            var purchaseRule = await _context.PurchaseRules.FindAsync(id);
            if (purchaseRule == null)
            {
                return NotFound();
            }

            _context.PurchaseRules.Remove(purchaseRule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseRuleExists(int id)
        {
            return (_context.PurchaseRules?.Any(e => e.PurchaseRuleId == id)).GetValueOrDefault();
        }
    }
}
