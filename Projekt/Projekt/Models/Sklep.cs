using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Sklep
    {
        public int SklepId { get; set; }
        public int KategorieId { get; set; }
        [Required(ErrorMessage = "Wprowadz nazwe przedmiotu")]
        [StringLength(100)]
        public string Tytul { get; set; }
        [Required(ErrorMessage = "Wprowadz marke przedmiotu")]
        [StringLength(100)]
        public string Marka { get; set; }
        public DateTime DataDodania { get; set; }
        [StringLength(100)]
        public string NazwaPlikuObrazka { get; set; }
        public string OpisPrzedmiotu { get; set; }
        public decimal Cena { get; set; }
        public bool bestseller { get; set; }
        public bool Ukryty { get; set; }
        public string OpisSkrocony { get; set; }

        public Kategoria Kategoria { get; set; }
    }
}