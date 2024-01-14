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
    public class GenericProductsController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public GenericProductsController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: api/GenericProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenericProduct>>> GetGenericProducts()
        {
          if (_context.GenericProducts == null)
          {
              return NotFound();
          }
            return await _context.GenericProducts.ToListAsync();
        }

        // GET: api/GenericProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenericProduct>> GetGenericProduct(int id)
        {
          if (_context.GenericProducts == null)
          {
              return NotFound();
          }
            var genericProduct = await _context.GenericProducts.FindAsync(id);

            if (genericProduct == null)
            {
                return NotFound();
            }

            return genericProduct;
        }

        // PUT: api/GenericProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenericProduct(int id, GenericProduct genericProduct)
        {
            if (id != genericProduct.GenericProductId)
            {
                return BadRequest();
            }

            _context.Entry(genericProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenericProductExists(id))
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

        // POST: api/GenericProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GenericProduct>> PostGenericProduct(GenericProduct genericProduct)
        {
          if (_context.GenericProducts == null)
          {
              return Problem("Entity set 'RestaurantDbContext.GenericProducts'  is null.");
          }
            _context.GenericProducts.Add(genericProduct);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGenericProduct", new { id = genericProduct.GenericProductId }, genericProduct);
        }

        // DELETE: api/GenericProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenericProduct(int id)
        {
            if (_context.GenericProducts == null)
            {
                return NotFound();
            }
            var genericProduct = await _context.GenericProducts.FindAsync(id);
            if (genericProduct == null)
            {
                return NotFound();
            }

            _context.GenericProducts.Remove(genericProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GenericProductExists(int id)
        {
            return (_context.GenericProducts?.Any(e => e.GenericProductId == id)).GetValueOrDefault();
        }
    }
}
