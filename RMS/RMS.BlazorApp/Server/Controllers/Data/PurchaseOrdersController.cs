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
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public PurchaseOrdersController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: api/PurchaseOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrders()
        {
          if (_context.PurchaseOrders == null)
          {
              return NotFound();
          }
            return await _context.PurchaseOrders.ToListAsync();
        }

        // GET: api/PurchaseOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrder(int id)
        {
          if (_context.PurchaseOrders == null)
          {
              return NotFound();
          }
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return purchaseOrder;
        }

        [HttpGet("PurchaseDetails/Include")]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrderWithPurchaseDetails() 
        {
            if (_context.PurchaseOrders == null) 
            {
                return NotFound();
            }
            return await _context.PurchaseOrders.Include(x => x.PurchaseDetails).ToListAsync();
        }

        [HttpGet("PurchaseDetails/{id}/Include")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrderWithPurchaseDetails(int id) 
        {
            if (_context.PurchaseOrders == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders.Include(x => x.PurchaseDetails).FirstOrDefaultAsync(x => x.PurchaseOrderId == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }
            return purchaseOrder;
        }

        [HttpGet("PurchaseOrders")]
        public async Task<ActionResult<IEnumerable<PurchaseRule>>> GetPurchaseOrderPurchaseRule() 
        {
            var data = await _context.PurchaseRules.Include(x => x.PurchaseOrders).ToListAsync();

            return data;
        }

        [HttpGet("PurchaseRule/Include")]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrdersWithPurchaseRules() 
        {
            if (_context.PurchaseOrders == null)
            {
                return NotFound();
            }
            return await _context.PurchaseOrders.Include(x => x.PurchaseRules).ToListAsync();
        }

        //
        [HttpGet("PurchaseRules")]
        public async Task<ActionResult<IEnumerable<PurchaseRule>>> GetPurchaseRules()
        {
            return await _context.PurchaseRules.ToListAsync();
        }

        [HttpGet("{id}/PurchaseRule/Include")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrderWithPurchaseRule(int id)
        {
            if (_context.PurchaseOrders == null)
            {
                return NotFound();
            }
            var @purchaseOrder = await _context.PurchaseOrders.Include(x => x.PurchaseRules).FirstOrDefaultAsync(x => x.PurchaseOrderId == id);
             
            if (@purchaseOrder == null)
            {
                return NotFound();
            }

            return @purchaseOrder;
        }

        [HttpGet("Supplier/Include")]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrdersWithSupplier()
        {
            if (_context.PurchaseOrders == null) 
            {
                return NotFound();
            }
            return await _context.PurchaseOrders.Include(x => x.Suppliers).ToListAsync();
        }

        
        [HttpGet("{id}/Supplier/Include")]
        public async Task<ActionResult<PurchaseOrder>> GetPurchaseOrdersWithSupplier(int id) 
        {
            if (_context.PurchaseOrders == null)
            {
                return NotFound();
            }
            var @purchaseOrder = await _context.PurchaseOrders.Include(x => x.Suppliers).FirstOrDefaultAsync(x => x.PurchaseOrderId == id);

            if (@purchaseOrder == null)
            {
                return NotFound();
            }

            return @purchaseOrder;
        }

        [HttpGet("Suppliers")]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync(); 
        }

        // PUT: api/PurchaseOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.PurchaseOrderId)
            {
                return BadRequest();
            }

            _context.Entry(purchaseOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderExists(id))
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

        // POST: api/PurchaseOrders
        [HttpPost]
        public async Task<ActionResult<PurchaseOrder>> PostPurchaseOrder(PurchaseOrder purchaseOrder)
        {
          if (_context.PurchaseOrders == null)
          {
              return Problem("Entity set 'RestaurantDbContext.PurchaseOrders'  is null.");
          }
            _context.PurchaseOrders.Add(purchaseOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseOrder", new { id = purchaseOrder.PurchaseOrderId }, purchaseOrder);
        }

        // DELETE: api/PurchaseOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseOrder(int id)
        {
            if (_context.PurchaseOrders == null)
            {
                return NotFound();
            }
            var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            _context.PurchaseOrders.Remove(purchaseOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePurchaseOrder(int id)
        //{
        //    if (_context.PurchaseOrders == null)
        //    {
        //        return NotFound();
        //    }

        //    var purchaseOrder = await _context.PurchaseOrders.FindAsync(id);
        //    if (purchaseOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    // Remove related PurchaseDetails before deleting the PurchaseOrder
        //    foreach (var purchaseDetail in purchaseOrder.PurchaseDetails.ToList())
        //    {
        //        _context.PurchaseDetails.Remove(purchaseDetail);
        //    }

        //    _context.PurchaseOrders.Remove(purchaseOrder);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}


        private bool PurchaseOrderExists(int id)
        {
            return (_context.PurchaseOrders?.Any(e => e.PurchaseOrderId == id)).GetValueOrDefault();
        }
    }
}
