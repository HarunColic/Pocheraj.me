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
            ArrayList lista = new ArrayList();

            lista.Add(obj.PostID);
            lista.Add(obj.UserID);


            if (this.Exists(lista))
            {
                _db.Applications.Update(obj);
            }
            else {
                _db.Applications.Add(obj);
            }

            _db.SaveChanges();

            return obj;
        }

        public bool Exists(ArrayList list)
        {

            int userID = int.Parse(list[1].ToString());
            int postID = int.Parse(list[0].ToString());

            var model = _db.Applications.FirstOrDefault(s => s.UserID == userID && s.PostID == postID);

            if (model != null)
                return true;
            else
                return false;

        }

        public List<Application> GetAllWithFilter(ArrayList filters)
        {
            int postID = int.Parse(filters[0].ToString());

            return _db.Applications.Where(x => x.PostID == postID).Include("User").Include("Post").ToList();
        }
    }
}
