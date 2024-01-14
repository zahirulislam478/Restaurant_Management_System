using RMS.BlazorApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RMS.BlazorApp.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDetailsController : ControllerBase
    {
        private readonly RestaurantDbContext db;

        public PurchaseDetailsController(RestaurantDbContext db)  
        {
            this.db = db;
        }

        // GET: api/PurchaseDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDetail>>> GetPurchaseDetails() 
        {
            if (db.PurchaseDetails == null)
            {
                return NotFound();
            }
            return await db.PurchaseDetails.ToListAsync();
        }
        /// 
        /// Custom
        /// 
        [HttpGet("Item/Include")]
        public async Task<ActionResult<IEnumerable<PurchaseDetail>>> GetPurchaseDetailsWithItem()
        {
            if (db.PurchaseDetails == null)
            {
                return NotFound();
            }
            return await db.PurchaseDetails.Include(x => x.Item).ToListAsync();
        }


        // GET: api/PurchaseDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDetail>> GetPurchaseDetail(int id) 
        {
            if (db.PurchaseDetails == null) 
            {
                return NotFound();
            }
            var purchaseDetail = await db.PurchaseDetails.FindAsync(id);

            if (purchaseDetail == null)
            {
                return NotFound();
            }

            return purchaseDetail;
        }
        /// 
        /// Custom
        /// 
        [HttpGet("Item/{id}/Include")]
        public async Task<ActionResult<PurchaseDetail>> GetPurchaseDetailWithItem(int id)  
        {
            if (db.PurchaseDetails == null)
            {
                return NotFound();
            }
            var purchaseDetails = await db.PurchaseDetails.Include(x => x.Item).FirstOrDefaultAsync(x => x.PurchaseDetailId == id);

            if (purchaseDetails == null)
            {
                return NotFound();
            }

            return purchaseDetails;
        }
        /// 
        /// Custom
        /// 

        [HttpGet("Item/{id}")]
        public async Task<ActionResult<IEnumerable<PurchaseDetail>>> GetPurchaseDetailsByItemId(int id /* itemId */) 
        {
            if (db.PurchaseDetails == null)
            {
                return NotFound();
            }
            var purchaseDetails = await db.PurchaseDetails.Where(x => x.ItemId == id).ToListAsync();
            return purchaseDetails;
        }

        [HttpGet("PurchaseOrder/{id}")]
        public async Task<ActionResult<IEnumerable<PurchaseDetail>>> GetPurchaseDetailsByPurchaseOrderId(int id)
        {
            if (db.PurchaseDetails == null)
            {
                return NotFound();
            }
            var purchaseDetails = await db.PurchaseDetails.Where(x => x.PurchaseOrderId == id).ToListAsync();
            return purchaseDetails;
        } 

        [HttpGet("PurchaseOrder/Include")]
        public async Task<ActionResult<IEnumerable<PurchaseDetail>>> GetPurchaseDetailsWithPurchaseOrder()
        {
            if (db.PurchaseDetails == null)
            {
                return NotFound();
            }
            return await db.PurchaseDetails.Include(x => x.PurchaseOrderId).ToListAsync();
        }

        [HttpGet("PurchaseOrder/{id}/Include")]
        public async Task<ActionResult<PurchaseDetail>> GetPurchaseDetailWithPurchaseOrder(int id)
        {
            if (db.PurchaseDetails == null)
            {
                return NotFound();
            }
            var purchaseDetails = await db.PurchaseDetails.Include(x => x.PurchaseOrderId).FirstOrDefaultAsync(x => x.PurchaseDetailId == id);

            if (purchaseDetails == null)
            {
                return NotFound();
            }
            return purchaseDetails;
        }

        // PUT: api/PurchaseDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseDetail(int id, PurchaseDetail purchaseDetail)
        {
            if (id != purchaseDetail.PurchaseDetailId)
            {
                return BadRequest();
            }

            db.Entry(purchaseDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseDetailExists(id))
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

        // POST: api/PurchaseDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseDetail>> PostPurchaseDetail(PurchaseDetail purchaseDetail) 
        {
            if (db.PurchaseDetails == null)
            {
                return Problem("Entity set 'RestaurantDbContext.PurchaseDetails'  is null.");
            }
            db.PurchaseDetails.Add(purchaseDetail);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetPurchaseDetail", new { id = purchaseDetail.PurchaseDetailId }, purchaseDetail);
        }

        // DELETE: api/PurchaseDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseDetail(int id) 
        {
            if (db.PurchaseDetails == null)
            {
                return NotFound();
            }
            var purchaseDetail = await db.PurchaseDetails.FindAsync(id);
            if (purchaseDetail == null)
            {
                return NotFound();
            }

            db.PurchaseDetails.Remove(purchaseDetail);
            await db.SaveChangesAsync();

            return NoContent();
        }



        private bool PurchaseDetailExists(int id)
        {
            return (db.PurchaseDetails?.Any(e => e.PurchaseDetailId == id)).GetValueOrDefault();
        }
    }
}
