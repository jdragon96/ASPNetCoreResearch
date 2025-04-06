using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class PgBookRepository : EntityRepository<Book>, IBookRepository
    {
        private DatabaseContext _db;
        public PgBookRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Book obj)
        {
            _db.Books.Update(obj);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
