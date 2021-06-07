using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebMonitoring.Models
{
    [Table("TB_M_KinerjaPerbankan")]
    public class KinerjaPerbankan
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Periode { get; set; }
        public string SandiBank { get; set; }
        public double KreditKol1 { get; set; }
        public double KreditKol2 { get; set; }
        public double KreditKol3 { get; set; }
        public double KreditKol4 { get; set; }
        public double KreditKol5 { get; set; }
        public double Laba { get; set; }
        public double Modal { get; set; }
        public double TotalAset { get; set; }
        public double ATMR { get; set; }
        public double BebanOperasional { get; set; }
        public double PendapatanOperasional { get; set; }
        public double DanaPihakKetiga { get; set; }
        public double NPL { get; set; }
        public double ROE { get; set; }
        public double ROA { get; set; }
        public double LDR { get; set; }
        public double BOPO { get; set; }
        public double CAR { get; set; }
    }
}
