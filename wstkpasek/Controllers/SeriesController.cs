﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkp.Models.Database;
using wstkp.Models.SeriesModel;

namespace wstkp.Controllers
{
  [Route("api/series")]
  [ApiController]
  [Authorize]
  public class SeriesController : ControllerBase
  {
    private readonly AppDBContext _context;

    public SeriesController(AppDBContext context)
    {
      _context = context;
    }

    // GET: api/Series
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Series>>> GetSeries()
    {
      return await _context.Series.ToListAsync();
    }

    // GET: api/Series/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Series>> GetSeries(int id)
    {
      var series = await _context.Series.FindAsync(id);

      if (series == null)
      {
        return NotFound();
      }

      return series;
    }

    // PUT: api/Series/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSeries(int id, Series series)
    {
      if (id != series.SeriesId)
      {
        return BadRequest();
      }

      _context.Entry(series).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!SeriesExists(id))
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

    // POST: api/Series
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<Series>> PostSeries(Series series)
    {
      _context.Series.Add(series);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetSeries", new { id = series.SeriesId }, series);
    }

    // DELETE: api/Series/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Series>> DeleteSeries(int id)
    {
      var series = await _context.Series.FindAsync(id);
      if (series == null)
      {
        return NotFound();
      }

      _context.Series.Remove(series);
      await _context.SaveChangesAsync();

      return series;
    }

    private bool SeriesExists(int id)
    {
      return _context.Series.Any(e => e.SeriesId == id);
    }
  }
}
