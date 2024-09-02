namespace FindService.EF
{
    public class AdvertisementPhoto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AdvertisementId { get; set; }
        public Guid PhotoId { get; set; }
        public int Position { get; set; }
    }
}
