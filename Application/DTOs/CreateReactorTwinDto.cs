namespace ReactorTwinAPI.Application.DTOs
{
    public class CreateReactorTwinDto
    {
        public string Name { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string? Location { get; set; }
    }
}
