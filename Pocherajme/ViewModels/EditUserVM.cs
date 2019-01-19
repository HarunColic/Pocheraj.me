using Microsoft.AspNetCore.Mvc.Rendering;
using Pocherajme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.ViewModels
{
    public class EditUserVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public List<City> Cities { get; set; }
        public string PhoneNumber{ get; set; }
        public int? CityID { get; set; }
    }
}
