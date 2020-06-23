using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1Poprawa.DTOs.Responses
{
    public class TaskResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public string TeamName { get; set; }
        public string TasktypeName { get; set; }
    }
}
