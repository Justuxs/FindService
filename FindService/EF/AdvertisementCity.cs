namespace FindService.EF
{
    public class AdvertisementCity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AdvertisementId { get; set; }
        public Guid CityId { get; set; }
    }
}
