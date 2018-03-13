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

        public Activity(int id, int userid, int activitytypeid, int durationmilliseconds)
        {
            id = Id;
            userid = UserId;
            activitytypeid = ActivityTypeId;
            durationmilliseconds = DurationMilliseconds;
        }

        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ActivityTypeId { get; set; }
        public int DurationMilliseconds { get; set; }

    }
}
