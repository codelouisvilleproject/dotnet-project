using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetproject.Models
{
    public class Activity
    {
        public Activity()
        {
        }

        public Activity(Guid id, Guid userid, Guid activitytypeid, int durationmilliseconds)
        {
            id = Id;
            userid = UserId;
            activitytypeid = ActivityTypeId;
            durationmilliseconds = DurationMilliseconds;
        }

        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ActivityTypeId { get; set; }
        public int DurationMilliseconds { get; set; }
    }
}
