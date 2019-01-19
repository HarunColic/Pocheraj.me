using Microsoft.EntityFrameworkCore;
using Pocherajme.Data;
using Pocherajme.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pocherajme.Repositories
{
    public class UserRepository: IRepository<ApplicationUser>
    {
        private ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public ApplicationUser Get(int id)
        {
            return _db.Users.Include("City").FirstOrDefault(s=>s.Id == id);
        }
        public void Save(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetAll()
        {
            return _db.Users.ToList();
        }

    }
}
