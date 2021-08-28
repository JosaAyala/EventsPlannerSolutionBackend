using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.ContextDTO.SubcategoryDtos
{
    public class GetSubcategoryEventRequest
    {
        public int SubcategoryId { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }
    }
}
