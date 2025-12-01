namespace ReactorTwinAPI.Domain.Entities
{
    public class ReactorTwin
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
