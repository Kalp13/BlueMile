using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlueMile.Coc.Data;
using BlueMile.Coc.WebApi.Data;

namespace BlueMile.Coc.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ImageContext _context;

        public ImagesController(ImageContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageEntity>>> GetImages()
        {
            return await _context.ImageEntities.ToListAsync();
        }

        // GET: api/Images/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ImageEntity>> GetImageById(Guid id)
        {
            var imageEntity = await _context.ImageEntities.FindAsync(id);

            if (imageEntity == null)
            {
                return NotFound();
            }

            return imageEntity;
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateImage(Guid id, ImageEntity imageEntity)
        {
            if (id != imageEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(imageEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoesImageExists(id))
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

        // POST: api/Images
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("create")]
        public async Task<IActionResult> CreateImage(ImageEntity imageEntity)
        {
            _context.ImageEntities.Add(imageEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImageById), new { id = imageEntity.Id }, imageEntity);
        }

        // DELETE: api/Images/5
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ImageEntity>> DeleteImage(Guid id)
        {
            var imageEntity = await _context.ImageEntities.FindAsync(id);
            if (imageEntity == null)
            {
                return NotFound();
            }

            _context.ImageEntities.Remove(imageEntity);
            await _context.SaveChangesAsync();

            return imageEntity;
        }

        private bool DoesImageExists(Guid id)
        {
            return _context.ImageEntities.Any(e => e.Id == id);
        }
    }
}
