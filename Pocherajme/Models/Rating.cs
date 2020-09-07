using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Models
{
    public class Rating
    {
        [Key]
        public int RatingID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("Rater")]
        public int RaterID { get; set; }
        public ApplicationUser Rater { get; set; }
        public Post Post { get; set; }
        [ForeignKey(nameof(Post))]
        public int PostID { get; set; }
        public float RatingValue { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
