using FindService.Dto.AdvertisementType;
using FindService.Dto.City;
using FindService.Dto.PhotoDto;
using Microsoft.AspNetCore.Http;

namespace FindService.EF
{
    public class AdvertisementDtoResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public AdvertisementTypeResponseDto? AdvertisementType { get; set; }
        public List<CityResponseDto> Cities { get; set; }
        public PhotoDtoResponse? MainPhoto { get; set; }
        public List<PhotoDtoResponse> Photos { get; set; }
    }
}
