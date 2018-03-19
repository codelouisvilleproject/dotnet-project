using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_project.Controllers
{
    [Produces("application/json")]
    [Route("api/ActivityTypes")]
    public class ActivityTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivityTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns list of Activity Types
        /// </summary>
        /// <returns></returns>
        // GET: api/Activities
        [HttpGet]
        public IEnumerable<ActivityType> GetActivityType()
        {

            return _context.ActivityTypes;

        }

        /// <summary>
        /// Returns specific Activity Type given Activity Type Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivityType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var activity = await _context.ActivityTypes.SingleOrDefaultAsync(m => m.Id == id);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }
    }
}