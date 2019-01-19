using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pocherajme.Data;
using Pocherajme.Models;

namespace Pocherajme.Repositories
{
    public class ApplicationRepository : IRepository<Application>
    {

        private ApplicationDbContext _db;
        public ApplicationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Application Get(int id)
        {
            return _db.Applications.Include("Post").Include("User").First(s => s.ApplicationID == id);
        }

        public List<Application> GetAll()
        {
            return _db.Applications.ToList();
        }

        public Application Save(Application obj)
        {
            _db.Applications.Add(obj);
            _db.SaveChanges();

            return obj;
        }

        public bool Exists(ArrayList list)
        {
            var model = _db.Applications.FirstOrDefault(s => s.UserID == list.IndexOf(1) && s.PostID == list.IndexOf(0));

            if (model != null)
                return true;
            else
                return false;

        }

        public List<Application> GetAllWithFilter(ArrayList filters)
        {
            throw new NotImplementedException();
        }
    }
}
