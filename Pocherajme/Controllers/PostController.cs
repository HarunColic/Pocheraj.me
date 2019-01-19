using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocherajme.Models;
using Pocherajme.Repositories;
using System;
using System.Collections.Generic;

namespace Pocherajme.Controllers
{
    public class PostController : Controller
    {
        private IRepository<Post> _postRepo;
        private IRepository<TransportType> _TTRepo;
        public PostController(IRepository<Post> ps, IRepository<TransportType> TTRepo)
        {
            _postRepo = ps;
            _TTRepo = TTRepo;
        }
        public IActionResult Index()
        {
            List<Post> model = _postRepo.GetAll();
            return View("Index", model);
        }

        public IActionResult AddPost()
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("/Home");

            //to do add view model for add
            ViewData["TransportTypes"] = _TTRepo.GetAll();
            return View("AddPost");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePost(IFormCollection collection)
        {
            try
            {
                TransportType TT = _TTRepo.Get(int.Parse(collection["TransportType"]));
                string[] time = collection["TimeOfDeparture"].ToString().Split(":");
                int hour = int.Parse(time[0]);
                int minute = int.Parse(time[1]);
                DateTime dateOfd = DateTime.Parse(collection["DateOfDeparture"]);
                DateTime DateTimeDep = new DateTime(dateOfd.Year, dateOfd.Month, dateOfd.Day, hour, minute, 0);
                Post post = new Post();
                post.Title = collection["title"];
                post.Description = collection["description"];
                post.To = collection["to"];
                post.From = collection["from"];
                post.DateTimeOfDeparture = DateTimeDep;
                post.MaxPassengers = int.Parse(collection["MaxPassengers"]);
                post.Price = float.Parse(collection["price"]);
                post.EstimatedTravelTime = int.Parse(collection["ETA"]);
                post.Car = collection["car"];
                post.TransportTypeID = TT.TransportTypeID;
                post.TypeOfTransport = TT;
                post.CreatedAt = DateTime.Now;
                post.IsPotraznja = false;
                _postRepo.Save(post);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return Redirect("/Home/Error");
            }
        }
    }
}