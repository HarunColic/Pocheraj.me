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
        IRepository<City> _cityRepo;
        IRepository<Post> _postRepo;
        public UserController(IRepository<ApplicationUser> userRepository, UserManager<ApplicationUser> userManager, IRepository<Post> postRepo, IRepository<City> cityRepo)
        {
            _postRepo = postRepo;
            _userManager = userManager;    
            _userRepo = userRepository;
            _cityRepo = cityRepo;
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
        public IActionResult Edit(IFormCollection colletion)
        {
            return RedirectToAction(nameof(Index));
        }

    }
}