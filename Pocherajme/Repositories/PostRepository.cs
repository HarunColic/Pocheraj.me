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
            if (_db.Posts.FirstOrDefault(x => x.PostID == int.Parse(list[0].ToString())) != null)
                return true;
            else
                return false;
        }

        public Post Get(int id)
        {
            return _db.Posts.Include("TypeOfTransport").First(s => s.PostID == id);
        }
        public List<Post> GetAll()
        {

            return _db.Posts.Where(x => !x.Completed).ToList();
        }

        public List<Post> GetAllWithFilter(ArrayList filters)
        {
            int UserID = 0;

            if (filters.Count > 1)
                UserID = int.Parse(filters[1].ToString());
            
                
            bool IsPotraznja = int.Parse(filters[0].ToString()) == 0;//ako je 0 onda je zatrazena potrazna, ako je false onda je ponuda
            try
            {
                if (UserID != 0 && int.Parse(filters[0].ToString()) != 3)
                    return _db.Posts.Where(p => p.IsPotraznja == IsPotraznja && p.ApplicationUserID == UserID).ToList();
                else if(UserID != 0 && int.Parse(filters[0].ToString()) == 3)
                {
                    var apps = _db.Applications.Where(x => x.UserID == UserID).ToList();

                    List<Post> posts = new List<Post>();

                    foreach(var i in apps)
                    {
                        posts.Add(_db.Posts.Find(i.PostID));
                    }

                    return posts;
                }
                else
                    return _db.Posts.Where(p => p.IsPotraznja == IsPotraznja).ToList();
            }
            catch
            {
                return _db.Posts.Where(s => s.ApplicationUserID == UserID).ToList();
                //log error
            }
        }

        public Post Save(Post post)
        {

            ArrayList lista = new ArrayList();

            lista.Add(post.PostID);

            if (this.Exists(lista))
                _db.Posts.Update(post);
            else
                _db.Posts.Add(post);

            _db.SaveChanges();

            return post;
        }
    }
}
