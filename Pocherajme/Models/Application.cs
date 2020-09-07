using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Models
{
    public class Application 
    {   
        public void changeState()
        {
            if (Accepted)
                Accepted = false;
            else
                Accepted = true;
        }

        [Key]
        public int ApplicationID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ForeignKey("Post")]
        public int PostID { get; set; }
        public ApplicationUser User { get; set; }
        public Post Post { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Accepted { get; set; }
    }
}
