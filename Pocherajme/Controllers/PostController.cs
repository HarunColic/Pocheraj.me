
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocherajme.Models;
using Pocherajme.Repositories;
using Pocherajme.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Pocherajme.Controllers
{
    public class PostController : Controller
    {
        private IRepository<Post> _postRepo;
        private IRepository<TransportType> _TTRepo;
        private IRepository<Application> _applicationRepo;
        private IRepository<Rating> _ratingsRepo;
        private UserManager<ApplicationUser> _user;
        public PostController(IRepository<Rating> ratingsRepo, IRepository<Post> ps, IRepository<TransportType> TTRepo, UserManager<ApplicationUser> user, IRepository<Application> app)
        {
            _user = user;
            _postRepo = ps;
            _TTRepo = TTRepo;
            _applicationRepo = app;
            _ratingsRepo = ratingsRepo;
        }
        public IActionResult Index()
        {
            List<Post> model = _postRepo.GetAll();
            return View("Index", model);
        }
        
        public IActionResult Create(int potraznja)
        {
            if (!User.Identity.IsAuthenticated)
                return Redirect("/Home");

            ViewData["Potraznja"] = potraznja;
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
            post.ApplicationUserID = int.Parse(_user.GetUserId(User));
            return _postRepo.Save(post);
        }

        public IActionResult ShowPost(int id)
        {
            ArrayList lista = new ArrayList();
            lista.Add(id);

            var applications = _applicationRepo.GetAllWithFilter(lista);
            var ex = applications.Exists(x => x.UserID == int.Parse(_user.GetUserId(User)));

            var post = _postRepo.Get(id);

            ArrayList arr = new ArrayList();
            arr.Add(post.ApplicationUserID);
            arr.Add(int.Parse(_user.GetUserId(User)));
            arr.Add(post.PostID);
            var rex = _ratingsRepo.Exists(arr);
            bool acc = false;

            if (ex)
            {
                acc = applications.Where(x => x.UserID == int.Parse(_user.GetUserId(User))).FirstOrDefault().Accepted;
            }

            Rating rating = null;

            if (rex)
            {
                ArrayList rat = new ArrayList();

                rat.Add(_postRepo.Get(id).ApplicationUserID);

                var ratings = new List<Rating>();

                ratings = _ratingsRepo.GetAllWithFilter(lista);

                rating = ratings.Where(x => x.RaterID == int.Parse(_user.GetUserId(User)) && x.PostID == id).FirstOrDefault();
            }

            var model = new ShowPost
            {
                Post = _postRepo.Get(id),
                Applications = applications,
                Accepted = acc,
                Ocjena = rating
            };

            return View(model);
        }

        public IActionResult Search(string title, string start, string destination, float price)
        {

            var model = _postRepo.GetAll();
            if (String.IsNullOrEmpty(title) == false)
                model = model.Where(x => x.Title == title).ToList();

            if (String.IsNullOrEmpty(start) == false)
                model = model.Where(x => x.From == start).ToList();

            if (String.IsNullOrEmpty(destination) == false)
                model = model.Where(x => x.To == destination).ToList();

            if (price > 0)
                model = model.Where(x => x.Price >= price).ToList();

            return View("Index", model);
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

            if (_applicationRepo.Exists(lista))
                return Redirect("/Home");

            Application aplikacija = new Application();

            aplikacija.PostID = id;
            aplikacija.UserID = int.Parse(_user.GetUserId(User));
            aplikacija.CreatedAt = DateTime.Now;

            _applicationRepo.Save(aplikacija);


            return RedirectToAction("Index");
        }

        public IActionResult GetPostsByType(int type)
        {

            ArrayList lista = new ArrayList();

            lista.Add(type);

            List<Post> model;

            if (type == 0 || type == 1)
               model = _postRepo.GetAllWithFilter(lista);
            else
               model = _postRepo.GetAll();
            

            return PartialView("PostsByType", model);
        }

        public IActionResult Accept(int apid)
        {

            var app = _applicationRepo.Get(apid);

            app.changeState();

            int postID = app.PostID;

            _applicationRepo.Save(app);

            return RedirectToAction("ShowPost", new { id = postID });
        }
        
        public IActionResult Rate(int PostID)
        {

            var rating = new Rating {

                PostID = PostID,
                RaterID = int.Parse(_user.GetUserId(User).ToString()),
                UserID = _postRepo.Get(PostID).ApplicationUserID,
                RatingValue = int.Parse(Request.Form["ocjena"]),
                Description = Request.Form["description"],
                CreatedAt = DateTime.Now
            };

            _ratingsRepo.Save(rating); 

            return RedirectToAction("ShowPost", new { id = PostID});
        }
        public IActionResult Complete(int PostID)
        {

            var post = _postRepo.Get(PostID);

            post.Completed = true;

            _postRepo.Save(post);

            return RedirectToAction(nameof(ShowPost), new { id = PostID });
        }
    }
}