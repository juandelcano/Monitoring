using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonitoring.Models
{
    [Table("TB_M_Employee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
