using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonitoring.Models
{
    [Table("TB_M_User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public bool IsUpdatePassword { get; set; }
        public Employee Employee { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
