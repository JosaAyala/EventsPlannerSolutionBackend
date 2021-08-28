using EventsPlanner.DomainContext.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPlanner.ContextDTO.UserDtos
{
    public class UserDto : BaseDto
    {
        public int UserId { get; set; }

        public string UserLogin { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public string RoleId { get; set; }

        public static UserDto From(User user)
        {
            if (user == null)
            {
                return new UserDto
                {
                    ErrorMessage = EventsPlannerStringKeys.itemNotExistMessageKey,
                };
            }

            return new UserDto
            {
                UserId = user.UserId,
                UserLogin = user.UserLogin,
                Name = user.Name,
                Lastname = user.Lastname,
                Password = user.Password,
                Email = user.Email,
                Active = user.Active,
                RoleId = user.RoleId
            };
        }
        public static List<UserDto> From(List<User> users)
        {
            if (users.Count() == 0)
            {
                return new List<UserDto>();
            }

            return (from user in users select From(user)).ToList();
        }

    }
}
