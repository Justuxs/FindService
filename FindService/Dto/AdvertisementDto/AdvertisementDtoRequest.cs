using FindService.Dto.AdvertisementType;
using FindService.Dto.City;
using FindService.Dto.PhotoDto;
using Microsoft.AspNetCore.Http;

namespace FindService.EF
{
    public class AdvertisementDtoRequest
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid AdvertisementTypeId { get; set; }
        public List<Guid> CitiesId { get; set; }
        public string MainPhotoBase64 { get; set; }
        public List<string> PhotosBase64 { get; set; }
    }
}
