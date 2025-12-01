namespace ReactorTwinAPI.Application.DTOs
{
    public class ReactorTwinDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
