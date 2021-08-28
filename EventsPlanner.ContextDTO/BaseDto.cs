using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.ContextDTO
{
    public abstract class BaseDto
    {
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}
