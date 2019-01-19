using Pocherajme.Data;
using Pocherajme.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Repositories
{
    public class CityRepository : IRepository<City>
    {
        ApplicationDbContext _db;
        public CityRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Exists(ArrayList list)
        {
            throw new NotImplementedException();
        }

        public City Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<City> GetAll()
        {
            return _db.Cities.ToList();
        }

        public List<City> GetAllWithFilter(ArrayList filters)
        {
            throw new NotImplementedException();
        }

        public City Save(City obj)
        {
            throw new NotImplementedException();
        }
    }
}
