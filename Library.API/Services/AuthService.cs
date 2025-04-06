using Library.Model;

namespace Library.API
{
    public class AuthService(IUserRepository repository, IConfiguration configuration) : IAuthService
    {
        Task<string> IAuthService.LoginAsync(User user)
        {
            throw new NotImplementedException();
        }

        Task<User> IAuthService.RegisterAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
