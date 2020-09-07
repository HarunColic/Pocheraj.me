using Pocherajme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.ViewModels
{
    public class ShowPost
    {
        public Post Post { get; set; }
        public List<Application> Applications { get; set; }
        public bool Accepted { get; set; }
        public Rating Ocjena { get; set; }
    }
}
