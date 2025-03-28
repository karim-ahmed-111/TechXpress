namespace TechXpress.Data.DTOs.Auth
{
    public class AuthResponseModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
