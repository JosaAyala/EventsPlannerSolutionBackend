using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.ContextDTO.UserDtos
{
    public class CreateEditUserRequest
    {
        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public string RoleId { get; set; }
    }
}
