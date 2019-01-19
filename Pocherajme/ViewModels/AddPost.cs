using Pocherajme.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.ViewModels
{
    public class AddPost
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Car { get; set; }
        public float Price { get; set; }
        public bool IsPotraznja { get; set; }
        public DateTime DateTimeOfDeparture { get; set; }
        List<TransportType> TransportTypes { get; set; }
    }
}
