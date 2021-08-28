using EventsPlanner.DomainContext.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.ContextDTO.SubcategoryDtos
{
    public class SubcategoryEventDto : BaseDto
    {
        public int SubcategoryId { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public decimal? Cost { get; set; }

        public static SubcategoryEventDto From(SubcategoryEvent subcategoryEvent)
        {
            if (subcategoryEvent == null)
            {
                return new SubcategoryEventDto
                {
                    ErrorMessage = EventsPlannerStringKeys.itemNotExistMessageKey,
                };
            }

            return new SubcategoryEventDto
            {
                SubcategoryId = subcategoryEvent.SubcategoryId,
                Description = subcategoryEvent.Description,
                CategoryId = subcategoryEvent.CategoryId,
                Cost = subcategoryEvent.Cost,
            };
        }
        public static List<SubcategoryEventDto> From(List<SubcategoryEvent> subcategoriesEvent)
        {
            if (subcategoriesEvent.Count() == 0)
            {
                return new List<SubcategoryEventDto>();
            }

            return (from subcategory in subcategoriesEvent select From(subcategory)).ToList();
        }
    }
}
