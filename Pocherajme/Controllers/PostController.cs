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
            DateTime DateOfDeparture, int MaxPassengers, float price, int ETA, string car, int TransportType)
        {

            TransportType TT = _TTRepo.Get(TransportType);

            Post post = new Post();

            post.Title = title;
            post.Description = description;
            post.To = to;
            post.From = from;
            post.DateTimeOfDeparture = DateOfDeparture;
            post.MaxPassengers = MaxPassengers;
            post.Price = price;
            post.EstimatedTravelTime = ETA;
            post.Car = car;
            post.TransportTypeID = TransportType;
            post.TypeOfTransport = TT;

            _postRepo.Save(post);

            return Redirect("/Post/AddPost");
        }
    }
}