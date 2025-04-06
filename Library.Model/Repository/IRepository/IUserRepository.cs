namespace Library.Model
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User obj);
        void Save();
    }
}