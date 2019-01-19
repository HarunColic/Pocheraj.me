using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pocherajme.Models;
using Pocherajme.Repositories;

namespace Pocherajme.Controllers
{
    public class UserController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        IRepository<ApplicationUser> _userRepo;
        public UserController(IRepository<ApplicationUser> userRepository, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;    
            _userRepo = userRepository;
        }
        public IActionResult Index()
        {
            ApplicationUser model = _userRepo.Get(int.Parse(_userManager.GetUserId(User))); 
            return View("Index", model);
        }
    }
}