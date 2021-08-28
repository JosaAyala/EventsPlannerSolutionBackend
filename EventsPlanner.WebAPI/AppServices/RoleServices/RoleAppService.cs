using EventsPlanner.ContextDTO.RoleDtos;
using EventsPlanner.DomainContext.DomainEntities;
using EventsPlanner.DomainContext.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlanner.WebAPI.AppServices.RoleServices
{
    public class RoleAppService
    {
        private readonly IGenericRepository<Role> _roleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public RoleAppService(IGenericRepository<Role> roleRepository,
                                            IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        internal async Task<RoleDto> GetRoleByIdAsync(GetRoleRequest request)
        {
            Role role = await _roleRepository.GetSingleAsync(x => x.RoleId == request.RoleId);

            return RoleDto.From(role);
        }

        internal async Task<List<RoleDto>> GetAllRolesAsync()
        {
            List<Role> roles = (await _roleRepository.GetAllAsync()).ToList();
            return RoleDto.From(roles);
        }
    }
}
