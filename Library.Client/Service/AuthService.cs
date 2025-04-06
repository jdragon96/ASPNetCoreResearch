using Library.Model;

namespace Library.Client
{
    public class AuthService
    {
        public bool IsAuthenticated { get; private set; } = false;
        public string? UserName { get; private set; }
        public event Action? OnAuthStateChanged;
        public User CurrentUser { get; set; }
        public void SetUser(User user)
        {
            IsAuthenticated = true;
            CurrentUser = user;
            NotifyAuthStateChanged();
        }

        public void Logout()
        {
            IsAuthenticated = false;
            UserName = null;
            NotifyAuthStateChanged();
        }

        private void NotifyAuthStateChanged()
        {
            OnAuthStateChanged?.Invoke();
        }
    }
}
