using FindService.EF.Context;
using FindService.EF;
using Microsoft.EntityFrameworkCore;
using FindService.Dto.User;
using FindService.Dto;
using FindService.Dto.City;
using FindService.Dto.AdvertisementType;

namespace FindService.Services.CityService
{
    public class AdvertisementTypeService
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new advertisementType if it doesn't already exist (case-insensitive check).
        /// </summary>
        /// <param name="AdvertisementTypeRequest">The advertisementType to create.</param>
        /// <returns>The created advertisementType.</returns>
        public async Task<APIResponse<AdvertisementTypeResponseDto?>> CreateAdvertisementTypeAsync(AdvertisementTypeRequestDto adType)
        {

            var existingType = await _context.AdvertisementTypes
                .FirstOrDefaultAsync(c => c.Name.ToLower() == adType.Name.ToLower());

            if (existingType != null)
            {
                return new APIResponse<AdvertisementTypeResponseDto?>
                {
                    IsSuccess = false,
                    Data = null,
                    ErrorMessage = "An ad with the same name already exists."
                };
            }
            var newAd = new AdvertisementType()
            {
                Name = adType.Name
            };

            _context.AdvertisementTypes.Add(newAd);
            await _context.SaveChangesAsync();

            var ad = new AdvertisementTypeResponseDto
            {
                Id = newAd.Id,
                Name = newAd.Name
            };

            return new APIResponse<AdvertisementTypeResponseDto?>
            {
                IsSuccess = true,
                Data = ad,
                ErrorMessage = null
            };
        }

        /// <summary>
        /// Deletes a advertisement type by its ID.
        /// </summary>
        /// <param name="id">The ID of the advertisement type to delete.</param>
        /// <returns>True if the advertisement type was deleted, false if not found.</returns>
        public async Task<APIResponse<bool>> DeleteAdvertisementTypeAsync(Guid id)
        {
            var existAd = await _context.AdvertisementTypes.FindAsync(id);
            if (existAd == null)
            {
                return new APIResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    ErrorMessage = "Ad not found."
                };
            }

            _context.AdvertisementTypes.Remove(existAd);
            await _context.SaveChangesAsync();

            return new APIResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                ErrorMessage = null
            };
        }

        public async Task<APIResponse<List<AdvertisementTypeResponseDto>>> GetAllAdvertisementTypesAsync()
        {
            var types = await _context.AdvertisementTypes
                .Select(a => new AdvertisementTypeResponseDto
                {
                    Id = a.Id,
                    Name = a.Name
                }).ToListAsync();

            return new APIResponse<List<AdvertisementTypeResponseDto>>
            {
                IsSuccess = true,
                Data = types,
                ErrorMessage = null
            };
        }
    }
}
