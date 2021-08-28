using EventsPlanner.DomainContext.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.ContextDTO.CategoryEventsDtos
{
    public class CategoryEventDto : BaseDto
    {
        public int CategoryId { get; set; }

        public string Description { get; set; }

        public static CategoryEventDto From(CategoryEvent categoryEvent)
        {
            if (categoryEvent == null)
            {
                return new CategoryEventDto
                {
                    ErrorMessage = EventsPlannerStringKeys.itemNotExistMessageKey,
                };
            }

            return new CategoryEventDto
            {
                CategoryId = categoryEvent.CategoryId,
                Description = categoryEvent.Description,
            };
        }
        public static List<CategoryEventDto> From(List<CategoryEvent> categoriesEvent)
        {
            if (categoriesEvent.Count() == 0)
            {
                return new List<CategoryEventDto>();
            }

            return (from category in categoriesEvent select From(category)).ToList();
        }
    }
}
