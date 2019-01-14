using Pocherajme.Data;
using Pocherajme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Repositories
{
    public class PostRepository:IRepository<Post>
    {
        private ApplicationDbContext _db;
        public PostRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Post Get(int id)
        {
            return _db.Posts.Find(id);
        }
        public List<Post> GetAll()
        {

            return _db.Posts.ToList();
        }
        public void Save(Post post)
        {
            _db.Posts.Add(post);
            _db.SaveChanges();
        }
    }
}
