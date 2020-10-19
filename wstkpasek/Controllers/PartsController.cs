using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkpasek.Models.Database;
using wstkpasek.Models.Exercises;

namespace wstkpasek.Controllers
{
  [Route("api/parts")]
  [ApiController]
  [Authorize]
  public class PartsController : ControllerBase
  {
    private readonly AppDBContext _context;

    public PartsController(AppDBContext context)
    {
      _context = context;
    }

    // GET: api/Parts
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Part>>> GetParts()
    {
      return await _context.Parts.ToListAsync();
    }

    // GET: api/Parts/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Part>> GetPart(string id)
    {
      var part = await _context.Parts.FindAsync(id);

      if (part == null)
      {
        return NotFound();
      }

      return part;
    }

    // PUT: api/Parts/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPart(string id, Part part)
    {
      if (id != part.Name)
      {
        return BadRequest();
      }

      _context.Entry(part).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!PartExists(id))
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

    // POST: api/Parts
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Part>> PostPart(Part part)
    {
      _context.Parts.Add(part);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException)
      {
        if (PartExists(part.Name))
        {
          return Conflict();
        }
        else
        {
          throw;
        }
      }

      return CreatedAtAction("GetPart", new { id = part.Name }, part);
    }

    // DELETE: api/Parts/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Part>> DeletePart(string id)
    {
      var part = await _context.Parts.FindAsync(id);
      if (part == null)
      {
        return NotFound();
      }

      _context.Parts.Remove(part);
      await _context.SaveChangesAsync();

      return part;
    }

    private bool PartExists(string id)
    {
      return _context.Parts.Any(e => e.Name == id);
    }
  }
}
