using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.ContextDTO.CategoryEventsDtos
{
    public class GetCategoryEventRequest
    {
        public int CategoryId { get; set; }

        public string Description { get; set; }
    }
}
