using Pocherajme.Data;
using Pocherajme.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pocherajme.Repositories
{
    public class UserRepository: IRepository<ApplicationUser>
    {
        private ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext  db)
        {
            _db = db;
        }
        public ApplicationUser Get(int id)
        {
            return _db.Users.FirstOrDefault(s=>s.Id == id);
        }
        public ApplicationUser Save(ApplicationUser obj)
        {
            return obj;
        }
        public List<ApplicationUser> GetAll()
        {
            return _db.Users.ToList();
        }

        public bool Exists(ArrayList list)
        {
            throw new System.NotImplementedException();
        }
    }
}
