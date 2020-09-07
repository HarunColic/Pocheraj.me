using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pocherajme.Models
{
    public class ApplicationUser : IdentityUser<int>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey("City")]
        public int? CityID { get; set; }
        public City City { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt{ get; set; }
    }
    public class AppRole : IdentityRole<int>
    {

    }

}