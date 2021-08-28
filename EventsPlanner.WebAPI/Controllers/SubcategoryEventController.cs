using EventsPlanner.ContextDTO.SubcategoryDtos;
using EventsPlanner.WebAPI.AppServices.SubcategoryEventServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventsPlanner.WebAPI.Controllers
{        
    [Route("api/subcategory-event")]
    [ApiController]
    public class SubcategoryEventController : ControllerBase
    {
        private readonly SubcategoryEventAppService _subcategoryEventAppService;
        public SubcategoryEventController(SubcategoryEventAppService subcategoryEventAppService)
        {
            _subcategoryEventAppService = subcategoryEventAppService;
        }

        [HttpGet("get-all")]
        public async Task<ActionResult> GetAllSubcategoriesEventAsync()
        {
            List<SubcategoryEventDto> subcategories = await _subcategoryEventAppService.GetAllSubcategoriesEventAsync();
            return Ok(subcategories);
        }

        [HttpGet("by-id-category")]
        public async Task<ActionResult> GetSubcategoriesEventByCategoryIdAsync([FromQuery]GetSubcategoryEventRequest request)
        {
            List<SubcategoryEventDto> subcategories = await _subcategoryEventAppService.GetSubcategoriesEventByCategoryIdAsync(request);
            return Ok(subcategories);
        }

        [HttpPost]
        public ActionResult CreateNewSubcategoryEventAsync([FromBody]CreateEditSubcategoryEventRequest request)
        {
            SubcategoryEventDto response = _subcategoryEventAppService.CreateNewSubcategoryEventAsync(request);
            return Ok(response);
        }

        [HttpPut]
        public ActionResult UpdateSubcategoryEventAsync([FromBody] CreateEditSubcategoryEventRequest request)
        {
            SubcategoryEventDto response = _subcategoryEventAppService.UpdateSubcategoryEventAsync(request);
            return Ok(response);
        }

        [HttpDelete]
        public ActionResult DeleteSubcategoryEventAsync([FromBody] CreateEditSubcategoryEventRequest request)
        {
            SubcategoryEventDto response = _subcategoryEventAppService.DeleteSubcategoryEventAsync(request.SubcategoryId);
            return Ok(response);
        }
    }
}
