using Library.Model;

namespace Library.API
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(User user);
        Task<string> LoginAsync(User user);
    }
}
