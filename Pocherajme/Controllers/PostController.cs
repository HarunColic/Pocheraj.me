using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocherajme.Models;
using Pocherajme.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Pocherajme.Controllers
{
    public class PostController : Controller
    {
        private IRepository<Post> _postRepo;
        private IRepository<TransportType> _TTRepo;
        private IRepository<Application> _applicationRepo;
        private UserManager<ApplicationUser> _user;
        public PostController(IRepository<Post> ps, IRepository<TransportType> TTRepo, UserManager<ApplicationUser> user, IRepository<Application> app)
        {
            _user = user;
            _postRepo = ps;
            _TTRepo = TTRepo;
            _applicationRepo = app; 
        }
        public IActionResult Index()
        {
            List<Post> model = _postRepo.GetAll();
            return View("Index", model);
        }
        
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("/Home");

            //to do add view model for add
            List<TransportType> model = _TTRepo.GetAll();

            return View("AddPost", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePotraznja(IFormCollection collection)
        {
            try
            {
                var model = this.savePost(collection, true);
                return RedirectToAction("ShowPost", new { id = model.PostID });

            }
            catch (Exception)
            {
                return Redirect("/Home/Error");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SavePonuda(IFormCollection collection)
        {

            try
            {

            var model = this.savePost(collection, false);
                return RedirectToAction("ShowPost", new { id = model.PostID});
            }
            catch (Exception)
            {
                Redirect("/Home/Error");
            }

            return View();
        }

        Post savePost(IFormCollection collection, bool potraznja)
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
            post.IsPotraznja = potraznja;

            return _postRepo.Save(post);
        }

        public IActionResult ShowPost(int id)
        {

            var model = _postRepo.Get(id);

            return View("ShowPost", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apply(int id)
        {

            if (!User.Identity.IsAuthenticated)
                return Redirect("/Home");

            ArrayList lista = new ArrayList();

            lista.Add(id);
            lista.Add(int.Parse(_user.GetUserId(User)));

            if (!_applicationRepo.Exists(lista))
                return Redirect("/Home");

            Application aplikacija = new Application();

            aplikacija.PostID = id;
            aplikacija.UserID = int.Parse(_user.GetUserId(User));
            aplikacija.CreatedAt = DateTime.Now;

            _applicationRepo.Save(aplikacija);


            return RedirectToAction("Index");
        }
    }
}
