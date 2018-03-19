using dotnet_project.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_project.Controllers
{
    [Produces("application/json")]
    [Route("api/Leader")]
    public class LeaderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaderController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Leader returns list of top ten users ranked by activities.DurationInMilliseconds
        /// </summary>
        /// <returns>Top 10 users ranked by DurationInMilliseconds</returns>
        /// GET: api/Users/{UserId}/Activities
        [HttpGet]
        public IEnumerable<Leader> GetLeader()
        {
            var leaderActivities = _context.Activities;

            var groupedActivities = leaderActivities.GroupBy(a => a.UserId);

            var leaderBoard = groupedActivities.Select(x => new Leader
            {
                UserId = x.Key,
                Metric = x.Sum(a => a.DurationMilliseconds).ToString(),
                ActivityCount = x.Count()
            })
            .OrderBy(x => x.Metric)
            .Take(10);

            // Group the activities using UserId as the key value 

            return leaderBoard;
        }
    }
}
