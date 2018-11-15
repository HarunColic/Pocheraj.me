using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Models
{
    public class PostImages
    {
        [Key]
        [ForeignKey("Post")]
        public int PostID { get; set; }
        public Post Post { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
