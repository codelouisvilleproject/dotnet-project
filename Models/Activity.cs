using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetproject.Models
{
    public class Activity
    {
        //public Activity()
        //{
        //}

        public Activity(int id, int userid, int activitytypeid, int durationmilliseconds)
        {
            id = Id;
            userid = UserId;
            activitytypeid = ActivityTypeId;
            durationmilliseconds = DurationMilliseconds;
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("useridgitkrak")]
        public int UserId { get; set; }
        public Activity ActivityType { get; set; }
        public int DurationMilliseconds { get; set; }

    }
}
