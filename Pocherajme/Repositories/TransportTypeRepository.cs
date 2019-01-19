using Pocherajme.Data;
using Pocherajme.Models;
using System;
using System.Collections;
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

        public bool Exists(ArrayList list)
        {
            throw new NotImplementedException();
        }

        public TransportType Get(int id)
        {

            return _db.TransportTypes.Find(id);
        }
        public List<TransportType> GetAll()
        {

            return _db.TransportTypes.ToList();
        }

        public List<TransportType> GetAllWithFilter(ArrayList filters)
        {
            throw new NotImplementedException();
        }

        public TransportType Save(TransportType tt)
        {

            _db.TransportTypes.Add(tt);
            _db.SaveChanges();

            return tt;
        }
    }
}
