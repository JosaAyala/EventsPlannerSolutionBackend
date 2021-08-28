using EventsPlanner.ContextDTO;
using EventsPlanner.ContextDTO.RoleDtos;
using EventsPlanner.ContextDTO.UserDtos;
using EventsPlanner.DomainContext.DomainEntities;
using EventsPlanner.DomainContext.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlanner.WebAPI.AppServices.UserServices
{
    public class UserAppService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserAppService(IGenericRepository<User> userRepository,
                                            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        internal async Task<List<UserDto>> GetAllUserAsync()
        {
            List<User> users = (await _userRepository.GetAllAsync()).ToList();
            return UserDto.From(users);
        }

        internal async Task<List<UserDto>> GetUsersByFilterAsync(GetUserRequest request)
        {
            List<User> users;
            if (!string.IsNullOrEmpty(request.UserLogin))
            {
                users = (await _userRepository.GetFilteredAsync(x => (x.UserLogin.Trim()).Contains(request.UserLogin.Trim()))).ToList();
                return UserDto.From(users);
            }
            if (!string.IsNullOrEmpty(request.RoleId))
            {
                users = (await _userRepository.GetFilteredAsync(x => (x.RoleId.Trim()).Contains(request.RoleId.Trim()))).ToList();
                return UserDto.From(users);
            }
            throw new NotImplementedException();
        }

        internal async Task<UserDto> GetUserToLoginAsync(LoginUserRequest request)
        {
            if (string.IsNullOrEmpty(request.UserLogin))
            {
                return null;
            }
            if (string.IsNullOrEmpty(request.UserPassword))
            {
                return null;
            }

            User userToLogin = await _userRepository.GetSingleAsync(x => x.UserLogin == request.UserLogin && x.Password == request.UserPassword);

            if (userToLogin != null)
            {
                return UserDto.From(userToLogin);
            }

            return null;
        }

        internal UserDto CreateNewUser(CreateEditUserRequest request)
        {
            User newUser = new User
            {
                UserLogin = request.UserLogin,
                Name = request.Name,
                Lastname = request.Lastname,
                Password = request.Password,
                Email = request.Email,
                Active = request.Active,
                RoleId = request.RoleId
            };

            _userRepository.Add(newUser);
            _unitOfWork.Commit();

            return new UserDto
            {
                SuccessMessage = EventsPlannerStringKeys.savedSuccessMessageKey,
            };
        }

        internal UserDto UpdateUser(CreateEditUserRequest request)
        {
            User user = _userRepository.GetSingle(x => x.UserLogin == request.UserLogin);

            if (user != null)
            {
                user.Name = request.Name;
                user.Lastname = request.Lastname;
                user.Password = request.Password;
                user.Email = request.Email;
                user.Active = request.Active;
                user.RoleId = request.RoleId;

                _userRepository.Modify(user);
                _unitOfWork.Commit();

                return new UserDto
                {
                    SuccessMessage = EventsPlannerStringKeys.savedSuccessMessageKey,
                };
            }

            return new UserDto
            {
                ErrorMessage = EventsPlannerStringKeys.itemNotExistMessageKey,
            };
        }

        internal UserDto DeleteUser(string userLogin)
        {
            User user = _userRepository.GetSingle(x => x.UserLogin == userLogin);

            if (user != null)
            {
                _userRepository.Remove(user);
                _unitOfWork.Commit();

                return new UserDto
                {
                    SuccessMessage = EventsPlannerStringKeys.removeSuccessMessageKey,
                };
            }

            return new UserDto
            {
                ErrorMessage = EventsPlannerStringKeys.errorSavedMessageKey,
            };
        }
    }
}
