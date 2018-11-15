using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Models
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public DateTime DateTimeOfDeparture { get; set; }
        public int MaxPassengers { get; set; }
        public float Price { get; set; }
        public int? EstimatedTravelTime { get; set; }
        public string Car { get; set; }
        [ForeignKey("TypeOfTransport")]
        public int TransportTypeID { get; set; }
        public TransportType TypeOfTransport { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
