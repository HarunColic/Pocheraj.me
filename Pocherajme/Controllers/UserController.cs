using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pocherajme.Models;
using Pocherajme.Repositories;

namespace Pocherajme.Controllers
{
    public class UserController : Controller
    {
        UserRepository _userRepo;
        public UserController(UserRepository userRepository)
        {
            _userRepo = userRepository;
        }
        public IActionResult Index()
        {
            ApplicationUser model = _userRepo.Get(1); 
            return View("Index", model);
        }
    }
}