using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.BlazorApp.Models;
using RMS.BlazorApp.Server.ViewModels.Item;
using RMS.BlazorApp.Shared.ViewModels.ProductReceive;

namespace RMS.BlazorApp.Server.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReceivesController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public ProductReceivesController(RestaurantDbContext context) 
        {
            _context = context;
        }

        // GET: api/ProductReceives
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReceive>>> GetProductReceives()
        {
            if (_context.ProductReceives == null)
            {
                return NotFound();
            }
            return await _context.ProductReceives.ToListAsync();
        }

        // GET: api/ProductReceives/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReceive>> GetProductReceive(int id)
        {
            if (_context.ProductReceives == null)
            {
                return NotFound();
            }
            var productReceive = await _context.ProductReceives.FindAsync(id);

            if (productReceive == null)
            {
                return NotFound();
            }

            return productReceive;
        }

        [HttpGet("ReceivedDetails/Include")]
        public async Task<ActionResult<IEnumerable<ProductReceive>>> GetProductReceivesWithReceivedDetails() 
        {
            if (_context.ProductReceives == null)
            {
                return NotFound();
            }
            return await _context.ProductReceives.Include(x => x.ReceivedDetails).ToListAsync();
        }

        [HttpGet("ReceivedDetails/{id}/Include")]
        public async Task<ActionResult<ProductReceive>> GetProductReceivesWithReceivedDetailsById(int id)  
        {
            if (_context.ProductReceives == null) 
            {
                return NotFound();
            }

            var productReceive = await _context.ProductReceives.Include(x => x.ReceivedDetails).FirstOrDefaultAsync(x => x.ProductReceiveId == id); 

            if (productReceive == null)
            {
                return NotFound();
            }
            return productReceive;
        }

        [HttpGet("VM/Include")]
        public async Task<ActionResult<IEnumerable<ProductReceiveViewModel>>> GetProductReceiveViewModelsWithReceivedDetails()
        {
            if (_context.ProductReceives == null)
            {
                return NotFound();
            }
            var productReceives = await _context.ProductReceives.Include(x => x.ReceivedDetails).ToListAsync();
            var data = productReceives.Select(x => new ProductReceiveViewModel
            {
                ProductReceiveId = x.ProductReceiveId,
                CompanyName = x.CompanyName,
                TotalAmount = x.TotalAmount,
                ReceivedDate = x.ReceivedDate.HasValue ? x.ReceivedDate.Value : DateTime.MinValue,
                PurchaseOrderId = x.PurchaseOrderId,
                SupplierId = x.SupplierId,
                CanDelete = !x.ReceivedDetails.Any(),
                ReceivedDetails = x.ReceivedDetails
            }).ToList();
            return data;
        }


        // PUT: api/ProductReceives/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductReceive(int id, ProductReceive productReceive) 
        {
            if (id != productReceive.ProductReceiveId)
            {
                return BadRequest();
            }

            _context.Entry(productReceive).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductReceiveExists(id))
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

        // POST: api/ProductReceives 
        [HttpPost]
        public async Task<ActionResult<ProductReceive>> PostProductReceive(ProductReceive productReceive) 
        {
            if (_context.ProductReceives == null)
            {
                return Problem("Entity set 'RestaurantDbContext.ProductReceives'  is null.");
            }
            _context.ProductReceives.Add(productReceive);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductReceive", new { id = productReceive.ProductReceiveId }, productReceive);
        }

        // DELETE: api/ProductReceives/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductReceive(int id) 
        {
            if (_context.ProductReceives == null)
            {
                return NotFound();
            }
            var productReceive = await _context.ProductReceives.FindAsync(id);
            if (productReceive == null)
            {
                return NotFound();
            }

            _context.ProductReceives.Remove(productReceive);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("CanDelete/{id}")]
        public async Task<ActionResult> CanDelete(int id /*product id */)
        {
            var item = await _context.ProductReceives.Include(x => x.ReceivedDetails).FirstOrDefaultAsync(x => x.ProductReceiveId == id);
            if (item == null) return NotFound();
            var canDelete = !item.ReceivedDetails.Any();
            return Ok(new { canDelete });
        }

        private bool ProductReceiveExists(int id) 
        {
            return (_context.ProductReceives?.Any(e => e.ProductReceiveId == id)).GetValueOrDefault();
        }

    }
}
