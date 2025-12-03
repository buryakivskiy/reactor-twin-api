namespace ReactorTwinAPI.Features.Users.Dtos
{
    public class RegisterRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool RequestSuper { get; set; } = false;
    }
}
