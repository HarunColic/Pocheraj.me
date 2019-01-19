using Microsoft.EntityFrameworkCore;
using Pocherajme.Data;
using Pocherajme.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private ApplicationDbContext _db;
        public PostRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Exists(ArrayList list)
        {
            throw new NotImplementedException();
        }

        public Post Get(int id)
        {
            return _db.Posts.Include("TypeOfTransport").First(s => s.PostID == id);
        }
        public List<Post> GetAll()
        {

            return _db.Posts.ToList();
        }

        public List<Post> GetAllWithFilter(ArrayList filters)
        {
            int UserID = int.Parse(filters[1].ToString());
            bool IsPotraznja = int.Parse(filters[0].ToString()) == 0;//ako je 0 onda je zatrazena potrazna, ako je false onda je ponuda
            try
            {
                return _db.Posts.Where(p => p.IsPotraznja == IsPotraznja && p.ApplicationUserID == UserID).ToList();
            }
            catch
            {
                return _db.Posts.Where(s => s.ApplicationUserID == UserID).ToList();
                //log error
            }
        }

        public Post Save(Post post)
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
            return post;
        }
    }
}
