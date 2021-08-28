using EventsPlanner.ContextDTO;
using EventsPlanner.ContextDTO.CategoryEventsDtos;
using EventsPlanner.DomainContext.DomainEntities;
using EventsPlanner.DomainContext.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlanner.WebAPI.AppServices.CategoryEventServices
{
    public class CategoryEventAppService
    {
        private readonly IGenericRepository<CategoryEvent> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryEventAppService(IGenericRepository<CategoryEvent> categoryRepository,
                                            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        internal async Task<List<CategoryEventDto>> GetAllCategoriesEventAsync()
        {
            List<CategoryEvent> categories = (await _categoryRepository.GetAllAsync()).ToList();

            return CategoryEventDto.From(categories);
        }

        internal async Task<CategoryEventDto> GetCategoriesEventByIdAsync(GetCategoryEventRequest request)
        {
            CategoryEvent category = await _categoryRepository.GetSingleAsync(x => x.CategoryId == request.CategoryId);

            return CategoryEventDto.From(category);
        }

        internal CategoryEventDto CreateNewCategoryEventAsync(CreateEditCategoryEventRequest request)
        {
            CategoryEvent newCategory = new CategoryEvent
            {
                CategoryId = request.CategoryId,
                Description = request.Description,
            };

            _categoryRepository.Add(newCategory);
            _unitOfWork.Commit();

            return new CategoryEventDto
            {
                SuccessMessage = EventsPlannerStringKeys.savedSuccessMessageKey,
            };
        }

        internal CategoryEventDto UpdateCategoryEventAsync(CreateEditCategoryEventRequest request)
        {
            CategoryEvent category = _categoryRepository.GetSingle(x => x.CategoryId == request.CategoryId);

            if (category != null)
            {
                category.Description = request.Description;

                _categoryRepository.Modify(category);
                _unitOfWork.Commit();

                return new CategoryEventDto
                {
                    SuccessMessage = EventsPlannerStringKeys.savedSuccessMessageKey,
                };
            }

            return new CategoryEventDto
            {
                ErrorMessage = EventsPlannerStringKeys.itemNotExistMessageKey,
            };
        }

        internal CategoryEventDto DeleteCategoryEventAsync(int id)
        {
            CategoryEvent category = _categoryRepository.GetSingle(x => x.CategoryId == id);

            if (category != null)
            {
                _categoryRepository.Remove(category);

                _unitOfWork.Commit();

                return new CategoryEventDto
                {
                    SuccessMessage = EventsPlannerStringKeys.removeSuccessMessageKey,
                };
            }

            return new CategoryEventDto
            {
                ErrorMessage = EventsPlannerStringKeys.errorSavedMessageKey,
            };
        }
    }
    
}
