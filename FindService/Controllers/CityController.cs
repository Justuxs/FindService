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
    public class CityController : ControllerBase
    {
        private readonly CityService _cityService;

        public CityController(CityService cityService)
        {
            _cityService = cityService;
        }


        /// <summary>
        /// Get all cities
        /// </summary>
        /// <returns>All cities list.</returns>
        [HttpGet()]
        public async Task<IActionResult> GetAllCities()
        {
            var result = await _cityService.GetAllCitiesAsync();
            return Ok(result);
        }
    }
}
