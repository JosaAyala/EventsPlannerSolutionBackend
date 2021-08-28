using EventsPlanner.ContextDTO;
using EventsPlanner.ContextDTO.SubcategoryDtos;
using EventsPlanner.DomainContext.DataContext;
using EventsPlanner.DomainContext.DomainEntities;
using EventsPlanner.DomainContext.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlanner.WebAPI.AppServices.SubcategoryEventServices
{
    public class SubcategoryEventAppService
    {
        private readonly IGenericRepository<SubcategoryEvent> _subcategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SubcategoryEventAppService(IGenericRepository<SubcategoryEvent> subcategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _subcategoryRepository = subcategoryRepository;
            _unitOfWork = unitOfWork;
        }

        internal async Task<List<SubcategoryEventDto>> GetAllSubcategoriesEventAsync()
        {
            List<SubcategoryEvent> subcategories = (await _subcategoryRepository.GetAllAsync()).ToList();
            return SubcategoryEventDto.From(subcategories);
        }

        internal async Task<List<SubcategoryEventDto>> GetSubcategoriesEventByCategoryIdAsync(GetSubcategoryEventRequest request)
        {
            List<SubcategoryEvent> subcategories = (await _subcategoryRepository.GetFilteredAsync(x => x.CategoryId == request.CategoryId)).ToList();
            return SubcategoryEventDto.From(subcategories);
        }

        internal SubcategoryEventDto CreateNewSubcategoryEventAsync(CreateEditSubcategoryEventRequest request)
        {
            SubcategoryEvent newSubcategory = new SubcategoryEvent
            {
                CategoryId = request.CategoryId,
                Description = request.Description,
                Cost = request.Cost,
            };

            _subcategoryRepository.Add(newSubcategory);
            _unitOfWork.Commit();

            return new SubcategoryEventDto
            {
                SuccessMessage = EventsPlannerStringKeys.savedSuccessMessageKey,
            };
        }

        internal SubcategoryEventDto UpdateSubcategoryEventAsync(CreateEditSubcategoryEventRequest request)
        {
            SubcategoryEvent subcategory = _subcategoryRepository.GetSingle(x => x.SubcategoryId == request.SubcategoryId);

            if (subcategory != null)
            {
                subcategory.CategoryId = request.CategoryId;
                subcategory.Description = request.Description;
                subcategory.Cost = request.Cost;

                _subcategoryRepository.Modify(subcategory);
                _unitOfWork.Commit();

                return new SubcategoryEventDto
                {
                    SuccessMessage = EventsPlannerStringKeys.savedSuccessMessageKey,
                };
            }

            return new SubcategoryEventDto
            {
                ErrorMessage = EventsPlannerStringKeys.itemNotExistMessageKey,
            };
        }

        internal SubcategoryEventDto DeleteSubcategoryEventAsync(int id)
        {
            SubcategoryEvent subcategory = _subcategoryRepository.GetSingle(x => x.SubcategoryId == id);

            if (subcategory != null)
            {
                _subcategoryRepository.Remove(subcategory);

                _unitOfWork.Commit();

                return new SubcategoryEventDto
                {
                    SuccessMessage = EventsPlannerStringKeys.removeSuccessMessageKey,
                };
            }

            return new SubcategoryEventDto
            {
                ErrorMessage = EventsPlannerStringKeys.errorSavedMessageKey,
            };
        }
    }
}
