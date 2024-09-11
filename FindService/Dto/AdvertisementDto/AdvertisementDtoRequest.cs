using Microsoft.AspNetCore.Http;

namespace FindService.EF
{
    public class AdvertisementDtoRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid AdvertisementTypeId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;
        public List<Guid> CitiesId { get; set; }
        public string MainPhoto { get; set; }
        public List<string> Photos { get; set; }
    }
}
