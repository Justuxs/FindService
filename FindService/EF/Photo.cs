namespace FindService.EF
{
    public class Photo
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generated GUID
        public string Base64 { get; set; }
    }
}
