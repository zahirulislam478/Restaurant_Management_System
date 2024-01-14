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
    public class MeasurementsController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public MeasurementsController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: api/Measurements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetMeasurements()
        {
          if (_context.Measurements == null)
          {
              return NotFound();
          }
            return await _context.Measurements.ToListAsync();
        }

        // GET: api/Measurements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Measurement>> GetMeasurement(int id)
        {
          if (_context.Measurements == null)
          {
              return NotFound();
          }
            var measurement = await _context.Measurements.FindAsync(id);

            if (measurement == null)
            {
                return NotFound();
            }

            return measurement;
        }

        [HttpGet("Items/Include")]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetItemsWithMeasurements() 
        {
            if (_context.Measurements == null)
            {
                return NotFound();
            }
            return await _context.Measurements.Include(x => x.Items).ToListAsync();
        }

        // PUT: api/Measurements/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeasurement(int id, Measurement measurement)
        {
            if (id != measurement.MeasurementId)
            {
                return BadRequest();
            }

            _context.Entry(measurement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeasurementExists(id))
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

        // POST: api/Measurements
        [HttpPost]
        public async Task<ActionResult<Measurement>> PostMeasurement(Measurement measurement)
        {
          if (_context.Measurements == null)
          {
              return Problem("Entity set 'RestaurantDbContext.Measurements'  is null.");
          }
            _context.Measurements.Add(measurement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeasurement", new { id = measurement.MeasurementId }, measurement);
        }

        // DELETE: api/Measurements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeasurement(int id)
        {
            if (_context.Measurements == null)
            {
                return NotFound();
            }
            var measurement = await _context.Measurements.FindAsync(id);
            if (measurement == null)
            {
                return NotFound();
            }

            _context.Measurements.Remove(measurement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeasurementExists(int id)
        {
            return (_context.Measurements?.Any(e => e.MeasurementId == id)).GetValueOrDefault();
        }
    }
}