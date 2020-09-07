using Pocherajme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.ViewModels
{
    public class UserProfilVM
    {
        public ApplicationUser User { get; set; }
        public List<Post> Potraznje { get; set; }
        public List<Post> Ponude { get; set; }
        public List<Post> Aplicirani { get; set; }
        public float Ocjena { get; set; }
    }
}
