using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_project.Models
{
    public class Leader
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Metric { get; set; }
        public int UserId { get; set; }
        public int ActivityCount { get; set; }
    }
}
