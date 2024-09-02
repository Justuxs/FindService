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
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly CityService _cityService;
        private readonly AdvertisementTypeService _advertisementTypeService;

        public AdminController(CityService cityService, AdvertisementTypeService advertisementTypeService)
        {
            _cityService = cityService;
            _advertisementTypeService = advertisementTypeService;
        }

        private bool IsAdmin()
        {
            var isAdminClaim = User.Claims.FirstOrDefault(c => c.Type == "isAdmin")?.Value;
            return bool.TryParse(isAdminClaim, out bool isAdmin) && isAdmin;
        }

        /// <summary>
        /// Create a new city.
        /// </summary>
        /// <param name="city">The city details to create.</param>
        /// <returns>The created city details.</returns>
        [HttpPost("cities")]
        public async Task<IActionResult> CreateCity([FromBody] CityRequestDto city)
        {
            if (!IsAdmin())
            {
                return Forbid("You are not authorized to perform this action.");
            }

            return Ok(_cityService.CreateCityAsync(city));
        }

        /// <summary>
        /// Delete a city by ID.
        /// </summary>
        /// <param name="id">The ID of the city to delete.</param>
        /// <returns>An action result indicating success or failure.</returns>
        [HttpDelete("cities/{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            if (!IsAdmin())
            {
                return Forbid("You are not authorized to perform this action.");
            }

            return Ok(await _cityService.DeleteCityAsync(id));
        }


        [HttpGet("cities")]
        public async Task<IActionResult> GetAllCities()
        {
            var result = await _cityService.GetAllCitiesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Create a new advertisement type.
        /// </summary>
        /// <param name="advertisementType">The advertisement type details to create.</param>
        /// <returns>The created advertisement type details.</returns>
        [HttpPost("advertisement-types")]
        public async Task<IActionResult> CreateAdvertisementType([FromBody] AdvertisementTypeRequestDto advertisementType)
        {
            if (!IsAdmin())
            {
                return Forbid("You are not authorized to perform this action.");
            }

            var result = await _advertisementTypeService.CreateAdvertisementTypeAsync(advertisementType);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// Delete an advertisement type by ID.
        /// </summary>
        /// <param name="id">The ID of the advertisement type to delete.</param>
        /// <returns>An action result indicating success or failure.</returns>
        [HttpDelete("advertisement-types/{id}")]
        public async Task<IActionResult> DeleteAdvertisementType(Guid id)
        {
            if (!IsAdmin())
            {
                return Forbid("You are not authorized to perform this action.");
            }

            var result = await _advertisementTypeService.DeleteAdvertisementTypeAsync(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("advertisement-types")]
        public async Task<IActionResult> GetAllAdvertisementTypes()
        {

            var result = await _advertisementTypeService.GetAllAdvertisementTypesAsync();
            return Ok(result);
        }

    }
}
