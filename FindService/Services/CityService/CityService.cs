using FindService.EF.Context;
using FindService.EF;
using Microsoft.EntityFrameworkCore;
using FindService.Dto.User;
using FindService.Dto;
using FindService.Dto.City;

namespace FindService.Services.CityService
{
    public class CityService
    {
        private readonly ApplicationDbContext _context;

        public CityService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new city if it doesn't already exist (case-insensitive check).
        /// </summary>
        /// <param name="city">The city to create.</param>
        /// <returns>The created city.</returns>
        public async Task<APIResponse<CityResponseDto?>> CreateCityAsync(CityRequestDto city)
        {
            var existingCity = await _context.Cities
                .FirstOrDefaultAsync(c => c.Name.ToLower() == city.Name.ToLower());

            if (existingCity != null)
            {
                return new APIResponse<CityResponseDto?>
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessage = "A city with the same name already exists."
                };
            }
            var newCity = new City()
            {
                Name = city.Name,
                Position = city.Position,
            };

            _context.Cities.Add(newCity);
            await _context.SaveChangesAsync();

            var cityResponseDto = new CityResponseDto
            {
                Id = newCity.Id,
                Name = newCity.Name,
                Position = newCity.Position
            };

            return new APIResponse<CityResponseDto?>
            {
                IsSuccess = true,
                Data = cityResponseDto,
                ErrorMessage = null
            };
        }

        /// <summary>
        /// Deletes a city by its ID.
        /// </summary>
        /// <param name="id">The ID of the city to delete.</param>
        /// <returns>True if the city was deleted, false if not found.</returns>
        public async Task<APIResponse<bool>> DeleteCityAsync(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                // Return a response indicating the city was not found
                return new APIResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    ErrorMessage = "City not found."
                };
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            // Return a response indicating the city was deleted successfully
            return new APIResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                ErrorMessage = null
            };
        }

        public async Task<APIResponse<List<CityResponseDto>>> GetAllCitiesAsync()
        {
            var cities = await _context.Cities
                .Select(c => new CityResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Position = c.Position
                }).ToListAsync();

            return new APIResponse<List<CityResponseDto>>
            {
                IsSuccess = true,
                Data = cities,
                ErrorMessage = null
            };
        }
    }
}
