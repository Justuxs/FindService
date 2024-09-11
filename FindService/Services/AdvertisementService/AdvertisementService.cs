using FindService.EF.Context;
using FindService.EF;
using Microsoft.EntityFrameworkCore;
using FindService.Dto.User;
using FindService.Dto;
using FindService.Dto.City;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FindService.Services.CityService
{
    public class AdvertisementService
    {
        private readonly ApplicationDbContext _context;
        private readonly AdvertisementTypeService _advertisementTypeService;
        private readonly CityService _cityService;
        private readonly PhotoService _photoService;

        public AdvertisementService(AdvertisementTypeService advertisementTypeService, ApplicationDbContext context, CityService cityService, PhotoService photoService)
        {
            _context = context;
            _advertisementTypeService = advertisementTypeService;
            _cityService = cityService;
            _photoService = photoService;
        }


        private async Task<AdvertisementDtoResponse> ToDtoResponseAsync(Advertisement advertisement)
        {
            AdvertisementDtoResponse advertisementDtoResponse = new AdvertisementDtoResponse();
            advertisementDtoResponse.Id = advertisement.Id;
            advertisementDtoResponse.UserId = advertisement.UserId;
            advertisementDtoResponse.Title = advertisement.Title;
            advertisementDtoResponse.Description = advertisement.Description;
            advertisementDtoResponse.Price = advertisement.Price;
            advertisementDtoResponse.Phone = advertisement.Phone;
            advertisementDtoResponse.Email = advertisement.Email;
            advertisementDtoResponse.IsActive = advertisement.IsActive;

            advertisementDtoResponse.AdvertisementType = await _advertisementTypeService.GetAdvertisementTypeByIdAsync(advertisement.AdvertisementTypeId);
            advertisementDtoResponse.Cities = await _cityService.GetAllAdvertisementCitiesAsync(advertisement.Id);
            advertisementDtoResponse.Photos = await _photoService.GetAllAdvertisementPhotosAsync(advertisement.Id);
            advertisementDtoResponse.MainPhoto = await _photoService.GetMainAdvertisementPhotoAsync(advertisement.Id);

            return advertisementDtoResponse;
        }

        private async Task<List<AdvertisementDtoResponse>> ToDtoResponseAsync(List<Advertisement> advertisements)
        {
            var tasks = advertisements.Select(advertisement => ToDtoResponseAsync(advertisement));
            var results =  await Task.WhenAll(tasks);
            return results.ToList();
        }


        public async Task<APIResponse<List<AdvertisementDtoResponse>>> GetAdvertisements()
        {
            try
            {
                var allAds = await _context.Advertisements.ToListAsync();
                var allAdsDto = await ToDtoResponseAsync(allAds);
                return new APIResponse<List<AdvertisementDtoResponse>>
                {
                    Data = allAdsDto,
                    IsSuccess = allAdsDto.Any() ? true : false,
                    ErrorMessage = allAdsDto.Any() ? "" : "No advertisements found."
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<List<AdvertisementDtoResponse>>
                {
                    Data = null,
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<APIResponse<AdvertisementDtoResponse>> GetAdvertisementByIdAsync(Guid id)
        {
            try
            {
                var advertisement = await _context.Advertisements
                    .FirstOrDefaultAsync(ad => ad.Id == id);

                if (advertisement == null)
                {
                    return new APIResponse<AdvertisementDtoResponse>
                    {
                        Data = null,
                        IsSuccess = false,
                        ErrorMessage = "Advertisement not found."
                    };
                }
                var advertisementDto = await ToDtoResponseAsync(advertisement);
                return new APIResponse<AdvertisementDtoResponse>
                {
                    Data = advertisementDto,
                    IsSuccess = true,
                    ErrorMessage = ""
                };
            }
            catch (Exception ex)
            {
                return new APIResponse<AdvertisementDtoResponse>
                {
                    Data = null,
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };
            }
        }

    }
}
