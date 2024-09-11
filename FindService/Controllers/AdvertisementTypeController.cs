using FindService.Dto;
using FindService.Dto.User;
using FindService.EF.Context;
using FindService.EF;
using FindService.Services.AuthService;
using FindService.Services.UserService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using FindService.Services.CityService;
using FindService.Dto.City;
using FindService.Dto.AdvertisementType;

namespace FindService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementTypeController : ControllerBase
    {
        private readonly AdvertisementTypeService _advertisementTypeService;

        public AdvertisementTypeController(AdvertisementTypeService advertisementTypeService)
        {
            _advertisementTypeService = advertisementTypeService;
        }


        /// <summary>
        /// Get all advertisement types
        /// </summary>
        /// <returns>All advertisement types list.</returns>
        [HttpGet()]
        public async Task<IActionResult> GetAllAdvertisementTypes()
        {
            var result = await _advertisementTypeService.GetAllAdvertisementTypesAsync();
            return Ok(result);
        }
    }
}
