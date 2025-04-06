using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class PgUserRepository : EntityRepository<User>, IUserRepository
    {
        private DatabaseContext _db;
        public PgUserRepository(DatabaseContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User obj)
        {
            _db.Users.Update(obj);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
