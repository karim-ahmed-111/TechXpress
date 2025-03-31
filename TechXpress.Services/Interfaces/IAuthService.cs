public interface IAuthService
{
    Task<string> RegisterAsync(string email, string password, string address);
    Task<string> LoginAsync(string email, string password);
}