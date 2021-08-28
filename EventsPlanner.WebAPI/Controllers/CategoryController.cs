using EventsPlanner.ContextDTO.CategoryEventsDtos;
using EventsPlanner.WebAPI.AppServices.CategoryEventServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsPlanner.WebAPI.Controllers
{
    [Route("api/category-event")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryEventAppService _categoryEventAppService;
        public CategoryController(CategoryEventAppService categoryEventAppService)
        {
            _categoryEventAppService = categoryEventAppService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAllCategoriesEventAsync()
        {
            List<CategoryEventDto> categories = await _categoryEventAppService.GetAllCategoriesEventAsync();
            return Ok(categories);
        }

        [HttpGet("by-id")]
        public async Task<ActionResult> GetCategoriesEventByIdAsync([FromQuery] GetCategoryEventRequest request)
        {
            CategoryEventDto category = await _categoryEventAppService.GetCategoriesEventByIdAsync(request);
            return Ok(category);
        }

        [HttpPost]
        public ActionResult CreateNewCategoryEventAsync([FromBody] CreateEditCategoryEventRequest request)
        {
            CategoryEventDto response = _categoryEventAppService.CreateNewCategoryEventAsync(request);
            return Ok(response);
        }

        [HttpPut]
        public ActionResult UpdateCategoryEventAsync([FromBody] CreateEditCategoryEventRequest request)
        {
            CategoryEventDto response = _categoryEventAppService.UpdateCategoryEventAsync(request);
            return Ok(response);
        }

        [HttpDelete]
        public ActionResult DeleteCategoryEventAsync([FromBody]CreateEditCategoryEventRequest request)
        {
            CategoryEventDto response = _categoryEventAppService.DeleteCategoryEventAsync(request.CategoryId);
            return Ok(response);
        }
    }
}
