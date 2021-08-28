using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.ContextDTO.SubcategoryDtos
{
    public class CreateEditSubcategoryEventRequest
    {
        public int SubcategoryId { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public decimal? Cost { get; set; }
    }
}
