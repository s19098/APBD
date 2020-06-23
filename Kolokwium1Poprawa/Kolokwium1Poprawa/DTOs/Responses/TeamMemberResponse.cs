using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1Poprawa.DTOs.Responses
{
    public class TeamMemberResponse
    {
        public string Email { get; set; }
        public int IdTeamMember { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        


        public ICollection<TaskResponse> CreatedTasks { get; set; }
        public ICollection<TaskResponse> AssignedTasks { get; set; }
       
    }
}
