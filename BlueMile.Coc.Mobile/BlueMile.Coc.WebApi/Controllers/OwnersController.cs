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
    public class OwnersController : ControllerBase
    {
        private readonly OwnerContext _context;

        public OwnersController(OwnerContext context)
        {
            _context = context;
        }

        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerEntity>>> GetOwners()
        {
            return await _context.OwnerEntities.ToListAsync();
        }

        // GET: api/Owners/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<OwnerEntity>> GetOwner(Guid? id)
        {
            var ownerEntity = await _context.OwnerEntities.FindAsync(id);

            if (ownerEntity == null)
            {
                return NotFound();
            }

            return ownerEntity;
        }

        // PUT: api/Owners/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateOwner(Guid id, OwnerEntity ownerEntity)
        {
            //if (id != ownerEntity.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(ownerEntity).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!OwnerExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();

            _context.OwnerEntities.Update(ownerEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOwner), new { id = ownerEntity.Id }, ownerEntity);
        }

        // POST: api/Owners
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("create")]
        public async Task<ActionResult<OwnerEntity>> CreateOwner(OwnerEntity ownerEntity)
        {
            _context.OwnerEntities.Add(ownerEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOwner), new { id = ownerEntity.Id }, ownerEntity);
        }

        // DELETE: api/Owners/5
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<OwnerEntity>> DeleteOwner(long? id)
        {
            var ownerEntity = await _context.OwnerEntities.FindAsync(id);
            if (ownerEntity == null)
            {
                return NotFound();
            }

            _context.OwnerEntities.Remove(ownerEntity);
            await _context.SaveChangesAsync();

            return ownerEntity;
        }

        private bool OwnerExists(Guid id)
        {
            return _context.OwnerEntities.Any(e => e.Id == id);
        }
    }
}
