using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.ContextDTO.UserDtos
{
    public class GetUserRequest
    {
        public int UserId { get; set; }
        public string UserLogin { get; set; }

        public string RoleId { get; set; }
    }
}
