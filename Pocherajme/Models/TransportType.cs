using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Models
{
    public class TransportType 
    {
        public int TransportTypeID { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
 