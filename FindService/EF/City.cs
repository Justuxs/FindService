namespace FindService.EF
{
    public class City
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Auto-generated GUID
        public string Name { get; set; }
        public int Position { get; set; }
    }
}
