using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Car { get; set; }
        public float Price { get; set; }
        public bool IsPotraznja { get; set; }
        public DateTime DateTimeOfDeparture { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? MaxPassengers { get; set; }
        public int? EstimatedTravelTime { get; set; }

        public TransportType TypeOfTransport { get; set; }
        [ForeignKey("TypeOfTransport")]
        public int? TransportTypeID { get; set; }
    }
}
