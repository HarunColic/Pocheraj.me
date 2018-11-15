using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pocherajme.Models
{
    public class UserMeta
    {
        [Key]
        [ForeignKey("User")]
        public int UserID { get; set; }
        public ApplicationUser User { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
