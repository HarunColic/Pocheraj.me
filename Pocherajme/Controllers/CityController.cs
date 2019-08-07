using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Pocherajme.Data;

namespace Pocherajme.Controllers
{
    public class CityController : Controller
    {

        ApplicationDbContext _db;
        public CityController(ApplicationDbContext db)
        {
            _db = db;
        }
        public string Seed()
        {
            List<string> gradovi = new List<string>() { "Mostar", "Sarajevo", "Tuzla", "Visoko", "Travnik", "Jajce" };
            foreach (var item in gradovi)
            {
                var exits = _db.Cities.FirstOrDefault(x => x.Name == item);
                if (exits is null)
                {
                    _db.Cities.Add(new Models.City() { Name = item, CreatedAt = DateTime.Now, UpdatedAt = null });
                }
            }
            _db.SaveChanges();
            return "Seed finished";
        }
    }

}