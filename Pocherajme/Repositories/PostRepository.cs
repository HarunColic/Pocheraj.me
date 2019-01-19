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
            try
            {
                if (filters.IndexOf(0) == 1)
                {
                    return _db.Posts.Include("Users").Where(p => p.IsPotraznja == false).ToList();
                }
                else if (filters.IndexOf(0) == 0)
                {
                    return _db.Posts.Include("Users").Where(p => p.IsPotraznja == true).ToList();
                }
            }
            catch
            {
                //log error
            }

           return _db.Posts.ToList();
        }

        public Post Save(Post post)
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
            return post;
        }
    }
}
