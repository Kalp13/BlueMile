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
    public class BoatsController : ControllerBase
    {
        private readonly BoatContext _context;

        public BoatsController(BoatContext context)
        {
            _context = context;
        }

        // GET: api/Boats
        [HttpGet("{ownerId}")]
        public async Task<ActionResult<IEnumerable<BoatEntity>>> GetBoats(Guid ownerId)
        {
            return await _context.BoatEntities.Where(x => x.OwnerId == ownerId).ToListAsync();
        }

        // GET: api/Boats/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<BoatEntity>> GetBoat(Guid id)
        {
            var boatEntity = await _context.BoatEntities.FindAsync(id);

            if (boatEntity == null)
            {
                return NotFound();
            }

            return boatEntity;
        }

        // PUT: api/Boats/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("update/{id}")]
        public async Task<ActionResult<BoatEntity>> UpdateBoat(Guid id, BoatEntity boatEntity)
        {
            //if (id != boatEntity.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(boatEntity).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!BoatExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return NoContent();
            _context.BoatEntities.Update(boatEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBoat), new { id = boatEntity.Id }, boatEntity);
        }

        // POST: api/Boats
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("create")]
        public async Task<ActionResult<BoatEntity>> CreateBoat(BoatEntity boatEntity)
        {
            _context.BoatEntities.Add(boatEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBoat), new { id = boatEntity.Id }, boatEntity);
        }

        // DELETE: api/Boats/5
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<BoatEntity>> DeleteBoat(Guid id)
        {
            var boatEntity = await _context.BoatEntities.FindAsync(id);
            if (boatEntity == null)
            {
                return NotFound();
            }

            _context.BoatEntities.Remove(boatEntity);
            await _context.SaveChangesAsync();

            return boatEntity;
        }

        private bool BoatExists(Guid id)
        {
            return _context.BoatEntities.Any(e => e.Id == id);
        }
    }
}