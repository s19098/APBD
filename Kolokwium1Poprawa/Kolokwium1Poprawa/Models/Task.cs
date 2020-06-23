using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1Poprawa.Models
{
    public class Task
    {
        public int IdTask { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int IdTeam { get; set; }
        public int IdTaskType { get; set; } 
        public int IdCreator { get; set; }
        public int IdAssignedTo { get; set; }
       
    }
}
