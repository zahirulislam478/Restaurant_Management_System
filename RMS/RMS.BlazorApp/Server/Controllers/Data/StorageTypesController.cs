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
    public class StorageTypesController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public StorageTypesController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: api/StorageTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageType>>> GetStorageTypes()
        {
          if (_context.StorageTypes == null)
          {
              return NotFound();
          }
            return await _context.StorageTypes.ToListAsync();
        }

        // GET: api/StorageTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageType>> GetStorageType(int id)
        {
          if (_context.StorageTypes == null)
          {
              return NotFound();
          }
            var storageType = await _context.StorageTypes.FindAsync(id);

            if (storageType == null)
            {
                return NotFound();
            }

            return storageType;
        }

        // PUT: api/StorageTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStorageType(int id, StorageType storageType)
        {
            if (id != storageType.StorageTypeId)
            {
                return BadRequest();
            }

            _context.Entry(storageType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StorageTypeExists(id))
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

        // POST: api/StorageTypes
        [HttpPost]
        public async Task<ActionResult<StorageType>> PostStorageType(StorageType storageType)
        {
          if (_context.StorageTypes == null)
          {
              return Problem("Entity set 'RestaurantDbContext.StorageTypes'  is null.");
          }
            _context.StorageTypes.Add(storageType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStorageType", new { id = storageType.StorageTypeId }, storageType);
        }

        // DELETE: api/StorageTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorageType(int id)
        {
            if (_context.StorageTypes == null)
            {
                return NotFound();
            }
            var storageType = await _context.StorageTypes.FindAsync(id);
            if (storageType == null)
            {
                return NotFound();
            }

            _context.StorageTypes.Remove(storageType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StorageTypeExists(int id)
        {
            return (_context.StorageTypes?.Any(e => e.StorageTypeId == id)).GetValueOrDefault();
        }
    }
}
