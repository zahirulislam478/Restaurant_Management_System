using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.BlazorApp.Models;
using Microsoft.AspNetCore.Authorization;
using RMS.BlazorApp.ViewModels;
using RMS.BlazorApp.Server.ViewModels.Item;
using System.Diagnostics.Metrics;

namespace RMS.BlazorApp.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="Staff,Admin")]
    public class ItemsController : ControllerBase
    {
        private readonly RestaurantDbContext db;
        private readonly IWebHostEnvironment env;

        public ItemsController(RestaurantDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            if (db.Items == null)
            {
                return NotFound();
            }
            return await db.Items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            if (db.Items == null)
            {
                return NotFound();
            }
            var item = await db.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpGet("PurchaseDetails/Include")]
        public async Task<ActionResult<IEnumerable<Item>>> GetItemsWithPurchaseDetails()
        {
            if (db.Items == null)
            {
                return NotFound();
            }
            return await db.Items.Include(x => x.PurchaseDetails).ToListAsync();
        }

        [HttpGet("PurchaseDetails/{id}/Include")]
        public async Task<ActionResult<Item>> GetItemWithPurchaseDetails(int id) 
        {
            if (db.Items == null)
            {
                return NotFound();
            }

            var item = await db.Items.Include(x => x.PurchaseDetails).FirstOrDefaultAsync(x => x.ItemId == id);

            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpGet("VM")]
        public async Task<ActionResult<IEnumerable<ItemViewModel>>> GetItemViewModels()
        {
            var data = await db.Items
                .Include(x => x.Measurements)
                .Include(x => x.Brands)
                .Include(x=> x.StorageType)
                .Include(x=> x.GenericProducts)
                .ToListAsync();

            List<ItemViewModel> items = new List<ItemViewModel>();

            foreach (var x in data)
            {
                var vm = new ItemViewModel
                {
                    ItemId = x.ItemId,
                    ItemName = x.ItemName,
                    Weight = x.Weight,
                    Picture = x.Picture,
                    IsAvailable = x.IsAvailable,
                    StorageTypeId = x.StorageTypeId,
                    StorageTypeName = x.StorageType!.StorageTypeName,
                    MeasurementId = x.MeasurementId,
                    MeasurementType = x.Measurements!.MeasurementType,
                    BrandId = x.BrandId,
                    BrandName = x.Brands!.BrandName,
                    GenericProductId = x.GenericProductId,
                    GenericProductName = x.GenericProducts!.GenericProductName,
                    PurchaseRuleId = x.PurchaseRuleId,
                    CanDelete = !x.PurchaseDetails.Any(),
                    PurchaseDetails = x.PurchaseDetails,
                    Inventories = x.Inventories
                };

                items.Add(vm); // Add the created ItemViewModel to the list
            }

            return items;
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.ItemId)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            if (db.Items == null)
            {
                return Problem("Entity set 'RestaurantDbContext.Items'  is null.");
            }
            db.Items.Add(item);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = item.ItemId }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            if (db.Items == null)
            {
                return NotFound();
            }
            var item = await db.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("PurchaseDetails/{id}")]
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

        //[HttpPost("Upload/{id}")]
        //public async Task<ActionResult<UploadResponse>> Upload(int id, IFormFile file)
        //{
        //    var item = await db.Items.FirstOrDefaultAsync(x => x.ItemId == id);
        //    if (item == null) return NotFound();
        //    string ext = Path.GetExtension(file.FileName);
        //    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
        //    string savePath = Path.Combine(this.env.WebRootPath, "Pictures", fileName);
        //    if (!Directory.Exists(Path.Combine(this.env.WebRootPath, "Pictures")))
        //    {
        //        Directory.CreateDirectory(Path.Combine(this.env.WebRootPath, "Pictures"));
        //    }
        //    FileStream fs = new FileStream(savePath, FileMode.Create);
        //    await file.CopyToAsync(fs);
        //    fs.Close();
        //    item.Picture = fileName;
        //    await db.SaveChangesAsync();
        //    return new UploadResponse { FileName = fileName };
        //}

        [HttpPost("Upload")]
        public async Task<UploadResponse> Upload(IFormFile file)
        {
            string ext = Path.GetExtension(file.FileName);
            string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
            string savePath = Path.Combine(env.WebRootPath, "Pictures", f);
            using FileStream fs = new FileStream(savePath, FileMode.Create);
            await file.CopyToAsync(fs);
            fs.Close();
            return new UploadResponse { FileName = f };
        }

        [HttpGet("CanDelete/{id}")]
        public async Task<ActionResult> CanDelete(int id /*product id */)
        {
            var item = await db.Items.Include(x => x.PurchaseDetails).FirstOrDefaultAsync(x => x.ItemId == id);
            if (item == null) return NotFound(); 
            var canDelete = !item.PurchaseDetails.Any();
            return Ok(new { canDelete });
        }

        private bool ItemExists(int id)
        {
            return (db.Items?.Any(e => e.ItemId == id)).GetValueOrDefault();
        }
    }
}
