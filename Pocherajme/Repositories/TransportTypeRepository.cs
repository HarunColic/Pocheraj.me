using Pocherajme.Data;
using Pocherajme.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pocherajme.Repositories
{
    public class TransportTypeRepository : IRepository<TransportType>
    {
        private ApplicationDbContext _db;

        public TransportTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public TransportType Get(int id)
        {

            return _db.TransportTypes.Find(id);
        }
        public List<TransportType> GetAll()
        {

            return _db.TransportTypes.ToList();
        }

        public void Save(TransportType tt)
        {

            _db.TransportTypes.Add(tt);
            _db.SaveChanges();
        }
    }
}
