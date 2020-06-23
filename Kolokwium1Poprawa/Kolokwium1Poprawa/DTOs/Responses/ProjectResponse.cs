using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1Poprawa.DTOs.Responses
{
    public class ProjectResponse
    {
        public int IdTeam { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
    }
}
