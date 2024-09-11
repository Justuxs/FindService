namespace FindService.EF
{
    public class Advertisement
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Guid AdvertisementTypeId { get; set; }
        public DateTime CreateTimeStamp { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
