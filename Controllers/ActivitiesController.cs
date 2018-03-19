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

        /// <summary>
        /// GetActivities returns a list of activities for a given UserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        // GET: api/Users/{UserId}/Activities
        [HttpGet]
        public IEnumerable<Activity> GetActivities(Guid UserId)
        {
            var allActivities = _context.Activities.Where(m => m.UserId == UserId);

            return allActivities;

        }

    /// <summary>
    /// GetActivity returns a given activity for a given UserId
    /// </summary>
    /// <param name="UserId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// 
    // GET: api/Users/{UserId}/Activities/5
    [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetActivity(Guid UserId, [FromRoute] Guid id)
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

        /// <summary>
        /// Update given Activity parameters for given UserId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="activity"></param>
        /// <returns></returns>
        // PUT: api//Users/{UserId}/Activities/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity([FromRoute] Guid id, [FromBody] Activity activity)
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


        /// <summary>
        /// Add new Activity to a given UserId 
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        // POST: api/Users/{UserId}/Activities/5
        [HttpPost]
        public async Task<IActionResult> PostActivity([FromBody] Activity activity, [FromRoute] Guid UserId)
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

        /// <summary>
        /// Delete a given activity for a given UserId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        // DELETE: apiUsers/{UserId}/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity([FromRoute] Guid id, [FromRoute] Guid UserId)
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

        private bool ActivityExists(Guid id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}