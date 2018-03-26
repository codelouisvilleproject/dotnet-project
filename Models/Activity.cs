using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using dotnet_project.Models;

namespace dotnetproject.Models
{
    [Table("activities")]
    public class Activity
    {
        public Activity()
        {
        }

        public Activity(int id, int userid, ActivityType activityType, int durationmilliseconds)
        {
            id = Id;
            userid = UserId;
            activityType = activityType;
            durationmilliseconds = DurationMilliseconds;
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("userid")]
        public int UserId { get; set; }
        [ForeignKey("activitytypeid")]
        public ActivityType ActivityType { get; set; }
        [Column("durationmilliseconds")]
        public int DurationMilliseconds { get; set; }

    }
}
