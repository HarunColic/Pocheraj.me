using Pocherajme.Data;
using Pocherajme.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Repositories
{
    public class RatingsRepository : IRepository<Rating>
    {

        private ApplicationDbContext _db;

        public RatingsRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public Rating Get(int id)
        {

            return _db.Ratings.Find(id);
        }

        public Rating Save(Rating rating)
        {

            ArrayList lista = new ArrayList();
            lista.Add(rating.UserID);
            lista.Add(rating.RaterID);
            lista.Add(rating.PostID);

            if (this.Exists(lista))
                _db.Ratings.Update(rating);
            else
                _db.Ratings.Add(rating);

            _db.SaveChanges();

            return rating;
        }

        public bool Exists(ArrayList list)
        {
            int userID = int.Parse(list[0].ToString());
            int raterID = int.Parse(list[1].ToString());
            int postID = int.Parse(list[2].ToString());

            var model = _db.Ratings.FirstOrDefault(s => s.UserID == userID && s.RaterID == raterID && s.PostID == postID);

            if (model != null)
                return true;
            else
                return false;

        }

        public List<Rating> GetAll()
        {
            return _db.Ratings.ToList();
        }

        public List<Rating> GetAllWithFilter(ArrayList lista)
        {
            var userID = int.Parse(lista[0].ToString());
            return _db.Ratings.Where(x => x.UserID == userID).ToList();

        }
    }
}
