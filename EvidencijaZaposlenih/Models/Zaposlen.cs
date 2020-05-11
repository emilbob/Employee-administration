using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EvidencijaZaposlenih.Models
{
    public class Zaposlen
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ImePrezime { get; set; }
        [Required]
        [Range(1951, 1991)]
        public int GodinaRodjenja { get; set; }
        [Range(2001, int.MaxValue)]
        public int GodinaZaposlenja { get; set; }
        [Range(2001, 9999)]
        public decimal Plata { get; set; }

        public int KompanijaId { get; set; }
        public Kompanija Kompanija { get; set; }
    }
}
