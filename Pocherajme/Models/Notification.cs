using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Models
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public ApplicationUser User { get; set; }
        public int Level { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
