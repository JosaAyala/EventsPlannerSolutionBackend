using EventsPlanner.DomainContext.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.ContextDTO.RoleDtos
{
    public class RoleDto : BaseDto
    {
        public string RoleId { get; set; }
        public string RoleDescription { get; set; }

        public static RoleDto From(Role role)
        {
            if (role == null)
            {
                return new RoleDto
                {
                    ErrorMessage = EventsPlannerStringKeys.itemNotExistMessageKey,
                };
            }

            return new RoleDto
            {
                RoleId = role.RoleId,
                RoleDescription = role.RoleDescription,
            };
        }
        public static List<RoleDto> From(List<Role> roles)
        {
            if (roles.Count() == 0)
            {
                return new List<RoleDto>();
            }

            return (from role in roles select From(role)).ToList();
        }
    }
}
