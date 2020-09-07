using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pocherajme.Data;

namespace Pocherajme.Controllers
{
    public class TaskTypeController : Controller
    {
        ApplicationDbContext _db;
        public TaskTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public string Seed()
        {
            List<string> vrstePrevoza = new List<string>() { "Auto", "Kamion", "Autobus", "Avion", "Voz", "Brod", "Splav" };
            foreach (var item in vrstePrevoza)
            {
                var exits = _db.TransportTypes.FirstOrDefault(x => x.Type == item);
                if(exits is null)
                {
                    _db.TransportTypes.Add(new Models.TransportType() { Type = item, Icon = item + ".svg", CreatedAt = DateTime.Now, UpdatedAt = null });
                }
            }
            _db.SaveChanges();
            return "Seed finished";
        }
    }
}