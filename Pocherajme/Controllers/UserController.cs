using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocherajme.Models;
using Pocherajme.Repositories;
using Pocherajme.ViewModels;

namespace Pocherajme.Controllers
{
    public class UserController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        IRepository<ApplicationUser> _userRepo;
        IRepository<Rating> _ratingsRepo;
        IRepository<City> _cityRepo;
        IRepository<Post> _postRepo;
        public UserController(IRepository<Rating> ratingsRepo, IRepository<ApplicationUser> userRepository, UserManager<ApplicationUser> userManager, IRepository<Post> postRepo, IRepository<City> cityRepo)
        {
            _postRepo = postRepo;
            _userManager = userManager;
            _userRepo = userRepository;
            _cityRepo = cityRepo;
            _ratingsRepo = ratingsRepo;
        }
        public IActionResult Index()
        {
            UserProfilVM model = new UserProfilVM();
            model.User = _userRepo.Get(int.Parse(_userManager.GetUserId(User)));
            ArrayList arr = new ArrayList();
            arr.Add(1);
            arr.Add(_userManager.GetUserId(User));
           
            model.Ponude = _postRepo.GetAllWithFilter(arr);
            arr.Insert(0, 0);
            model.Potraznje = _postRepo.GetAllWithFilter(arr);

            ArrayList list = new ArrayList();
            list.Add(3);
            list.Add(_userManager.GetUserId(User));

            model.Aplicirani = _postRepo.GetAllWithFilter(list);

            ArrayList lista = new ArrayList();

            lista.Add(int.Parse(_userManager.GetUserId(User)));

            var ratings = new List<Rating>();

            ratings = _ratingsRepo.GetAllWithFilter(lista);

            float sumaOcjena = 0;

            foreach(var r in ratings)
            {
                sumaOcjena += r.RatingValue;
            }

            if (ratings.Count() > 0)
                model.Ocjena = sumaOcjena / ratings.Count();
            else
                model.Ocjena = 0;

            return View("Index", model);
        }

        public IActionResult Edit()
        {
            EditUserVM model = new EditUserVM();
            var user = _userRepo.Get(int.Parse(_userManager.GetUserId(User)));
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.PhoneNumber = user.PhoneNumber;
            model.Address = user.Address;
            model.CityID = user.CityID;
            model.Cities = _cityRepo.GetAll();
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IFormCollection collection)
        {

            try
            {

                ApplicationUser user = _userRepo.Get(int.Parse(_userManager.GetUserId(User)));
                user.FirstName = collection["firstName"];
                user.LastName = collection["lastName"];
                user.PhoneNumber = collection["phone"];
                user.Address = collection["address"];
                user.CityID = int.Parse(collection["city"]);
                _userRepo.Save(user);
            }
            catch
            {
                return Redirect("/Home/Error");
            }

            return RedirectToAction(nameof(Index));
        }

    }
}