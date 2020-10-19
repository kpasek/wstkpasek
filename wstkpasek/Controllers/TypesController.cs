using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkp.Models.Database;
using wstkp.Models.Exercises;
using Type = wstkp.Models.Exercises.Type;

namespace wstkp.Controllers
{
  [Route("api/types")]
  [ApiController]
  [Authorize]
  public class TypesController : ControllerBase
  {
    private readonly AppDBContext _context;

    public TypesController(AppDBContext context)
    {
      _context = context;
    }

    // GET: api/Types
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Type>>> GetTypes()
    {
      return await _context.Types.ToListAsync();
    }

    // GET: api/Types/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Type>> GetType(string id)
    {
      var @type = await _context.Types.FindAsync(id);

      if (@type == null)
      {
        return NotFound();
      }

      return @type;
    }

    // PUT: api/Types/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutType(string id, Type @type)
    {
      if (id != @type.TypeName)
      {
        return BadRequest();
      }

      _context.Entry(@type).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TypeExists(id))
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

    // POST: api/Types
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Type>> PostType(Type @type)
    {
      _context.Types.Add(@type);
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateException)
      {
        if (TypeExists(@type.TypeName))
        {
          return Conflict();
        }
        else
        {
          throw;
        }
      }

      return CreatedAtAction("GetType", new { id = @type.TypeName }, @type);
    }

    // DELETE: api/Types/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Type>> DeleteType(string id)
    {
      var @type = await _context.Types.FindAsync(id);
      if (@type == null)
      {
        return NotFound();
      }

      _context.Types.Remove(@type);
      await _context.SaveChangesAsync();

      return @type;
    }

    private bool TypeExists(string id)
    {
      return _context.Types.Any(e => e.TypeName == id);
    }
  }
}
