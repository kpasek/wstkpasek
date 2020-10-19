using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkp.Models.Database;
using wstkp.Models.User;

namespace wstkp.Controllers
{
  [Route("api/weights")]
  [ApiController]
  [Authorize]
  public class WeightsController : ControllerBase
  {
    private readonly AppDBContext _context;

    public WeightsController(AppDBContext context)
    {
      _context = context;
    }

    // GET: api/Weights
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Weight>>> GetWeights()
    {
      return await _context.Weights.ToListAsync();
    }

    // GET: api/Weights/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Weight>> GetWeight(int id)
    {
      var weight = await _context.Weights.FindAsync(id);

      if (weight == null)
      {
        return NotFound();
      }

      return weight;
    }

    // PUT: api/Weights/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWeight(int id, Weight weight)
    {
      if (id != weight.WeightId)
      {
        return BadRequest();
      }

      _context.Entry(weight).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!WeightExists(id))
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

    // POST: api/Weights
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Weight>> PostWeight(Weight weight)
    {
      _context.Weights.Add(weight);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetWeight", new { id = weight.WeightId }, weight);
    }

    // DELETE: api/Weights/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Weight>> DeleteWeight(int id)
    {
      var weight = await _context.Weights.FindAsync(id);
      if (weight == null)
      {
        return NotFound();
      }

      _context.Weights.Remove(weight);
      await _context.SaveChangesAsync();

      return weight;
    }

    private bool WeightExists(int id)
    {
      return _context.Weights.Any(e => e.WeightId == id);
    }
  }
}
