using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMS.BlazorApp.Models;

namespace RMS.BlazorApp.Server.Controllers.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RestaurantDbContext _context;

        public RecipesController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            return await _context.Recipes.ToListAsync();
        }

        // GET: api/Recipes/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipes.FindAsync(id);

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        [HttpGet("RecipeIngredients/Include")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesWithRecipeIngredients() 
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            return await _context.Recipes.Include(x => x.RecipeIngredients).ToListAsync();
        }

        [HttpGet("RecipeIngredients/{id}/Include")]
        public async Task<ActionResult<Recipe>> GetRecipeWithRecipeIngredients(int id) 
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.Include(x => x.RecipeIngredients).FirstOrDefaultAsync(x => x.RecipeId == id);

            if (recipe == null)
            {
                return NotFound();
            }
            return recipe;
        }


        // PUT: api/Recipes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.RecipeId)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipes 
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'RestaurantDbContext.Recipes'  is null.");
            }
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return (_context.Recipes?.Any(e => e.RecipeId == id)).GetValueOrDefault();
        }
    }
}
