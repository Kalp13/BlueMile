using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlueMile.Coc.Data;
using BlueMile.Coc.WebApi.Data;
using System;

namespace BlueMile.Coc.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequiredItemsController : ControllerBase
    {
        private readonly RequiredItemContext _context;

        public RequiredItemsController(RequiredItemContext context)
        {
            _context = context;
        }

        // GET: api/RequiredItems
        [HttpGet("{boatId}")]
        public async Task<ActionResult<IEnumerable<RequiredItemEntity>>> GetRequiredItems(Guid boatId)
        {
            return await _context.RequiredItems.Where(x => x.BoatId == boatId).ToListAsync();
        }

        // GET: api/RequiredItems/5
        [HttpGet("itemId={id}")]
        public async Task<ActionResult<RequiredItemEntity>> GetRequiredItem(Guid id)
        {
            var requiredItemEntity = await _context.RequiredItems.FindAsync(id);

            if (requiredItemEntity == null)
            {
                return NotFound();
            }

            return requiredItemEntity;
        }

        // PUT: api/RequiredItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRequiredItem(Guid id, RequiredItemEntity requiredItemEntity)
        {
            if (id != requiredItemEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(requiredItemEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequiredItemExists(id))
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

        // POST: api/RequiredItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RequiredItemEntity>> CreateRequiredItem(RequiredItemEntity requiredItemEntity)
        {
            _context.RequiredItems.Add(requiredItemEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequiredItemEntity", new { id = requiredItemEntity.Id }, requiredItemEntity);
        }

        // DELETE: api/RequiredItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequiredItemEntity>> DeleteRequiredItem(long? id)
        {
            var requiredItemEntity = await _context.RequiredItems.FindAsync(id);
            if (requiredItemEntity == null)
            {
                return NotFound();
            }

            _context.RequiredItems.Remove(requiredItemEntity);
            await _context.SaveChangesAsync();

            return requiredItemEntity;
        }

        private bool RequiredItemExists(Guid id)
        {
            return _context.RequiredItems.Any(e => e.Id == id);
        }
    }
}
