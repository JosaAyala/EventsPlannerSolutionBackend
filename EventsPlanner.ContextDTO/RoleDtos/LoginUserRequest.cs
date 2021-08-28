using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.ContextDTO.RoleDtos
{
    public class LoginUserRequest
    {
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
    }
}
