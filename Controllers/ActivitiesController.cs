using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_project.Models;
using dotnetproject.Models;

namespace dotnet_project.Controllers
{
    [Produces("application/json")]
    [Route("api/Users/{UserId}/Activities")]
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Activities
        [AuthorizationUser]
        [HttpGet]
        public IEnumerable<Activity> GetActivities(int UserId)
        {
            var allActivities = _context.Activities.Where(m => m.UserId == UserId);

            return allActivities;

        }

        // GET: api/Activities/5
        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetActivity(int UserId, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activity = await _context.Activities.SingleOrDefaultAsync(m => m.Id == id && m.UserId == UserId);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        // PUT: api/Activities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity([FromRoute] int id, [FromBody] Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activity.Id)
            {
                return BadRequest();
            }

            _context.Entry(activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
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

        // POST: api/Activities
        [HttpPost]
        public async Task<IActionResult> PostActivity([FromBody] Activity activity, [FromRoute] int UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            activity.UserId = UserId;

            _context.Activities.Add(activity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActivity", new { id = activity.Id }, activity);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity([FromRoute] int id, [FromRoute] int UserId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var activity = await _context.Activities.SingleOrDefaultAsync(m => m.Id == id && m.UserId == UserId);

            if (activity == null)
            {
                return NotFound();
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return Ok(activity);
        }

        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}