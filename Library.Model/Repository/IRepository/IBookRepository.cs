namespace Library.Model
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book obj);
        void Save();
    }
}