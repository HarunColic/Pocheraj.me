using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocherajme.Models;
using Pocherajme.Repositories;
using System;

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
            ViewData["Posts"] = _postRepo.GetAll();
            return View();
        }

        public IActionResult AddPost()
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("/home");

            ViewData["TransportTypes"] = _TTRepo.GetAll();
            return View("AddPost");
        }

        public IActionResult SavePost(string title, string description, string to, string from, 
            DateTime DateOfDeparture, string TimeOfDeparture, int MaxPassengers, float price, int ETA, string car, int TransportType)
        {

            TransportType TT = _TTRepo.Get(TransportType);

            string [] time = TimeOfDeparture.Split(":");

            int hour = int.Parse(time[0]);
            int minute = int.Parse(time[1]);

            DateTime DateTimeDep = new DateTime(DateOfDeparture.Year, DateOfDeparture.Month, DateOfDeparture.Day, hour, minute, 0);

            Post post = new Post();

            post.Title = title;
            post.Description = description;
            post.To = to;
            post.From = from;
            post.DateTimeOfDeparture = DateTimeDep;
            post.MaxPassengers = MaxPassengers;
            post.Price = price;
            post.EstimatedTravelTime = ETA;
            post.Car = car;
            post.TransportTypeID = TransportType;
            post.TypeOfTransport = TT;
            post.CreatedAt = DateTime.Now;

            _postRepo.Save(post);

            return Redirect("/Post/AddPost");
        }
    }
}