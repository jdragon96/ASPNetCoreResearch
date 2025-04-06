namespace Library.Model
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public User UserModel { get; set; }
    }
}
