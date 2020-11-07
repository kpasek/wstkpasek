using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wstkpasek.Models.Database;
using wstkpasek.Models.User;

namespace wstkpasek.Controllers
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
        private string GetEmail()
        {
            return this.User.Identity.Name;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weight>>> GetWeights()
        {
            var email = GetEmail();
            return await _context.Weights.Where(w => w.UserEmail == email).ToListAsync();
        }
        [HttpGet("dates")]
        public async Task<ActionResult<IEnumerable<Weight>>> GetWeights(DateTime dateFrom, DateTime dateTo)
        {
            var email = GetEmail();
            dateFrom = dateFrom.Year == 1 ? DateTime.Now.AddMonths(-3) : dateFrom;
            dateTo = dateTo.Year == 1 ? DateTime.Now : dateTo;
            return await _context.Weights.Where(w => w.UserEmail == email && w.Date >= dateFrom && w.Date <= dateTo).OrderBy(o => o.Date).ToListAsync();
        }

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


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeight(int id, Weight weight)
        {
            if (id != weight.WeightId)
            {
                return BadRequest();
            }
            var email = GetEmail();
            weight.UserEmail = email;
            _context.Entry(weight).State = EntityState.Modified;

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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Weight>> PostWeight(Weight weight)
        {
            var email = GetEmail();
            weight.UserEmail = email;
            _context.Weights.Add(weight);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TypeExists(weight.WeightId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWeigth", new { id = weight.WeightId }, weight);
        }
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

        private bool TypeExists(int id)
        {
            return _context.Weights.Any(e => e.WeightId == id);
        }
    }
}
