using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Zamowienia
    {
        public int ZamowieniaId { get; set; }

        [Required(ErrorMessage = "Wprowadz Imie")]
        [StringLength(50)]
        public string Imie { get; set; }

        [Required(ErrorMessage = "Wprowadz nazwisko")]
        [StringLength(100)]
        public string Nazwisko { get; set; }

        [Required(ErrorMessage = "Wprowadz miasto")]
        [StringLength(100)]
        public string Miasto { get; set; }

        [Required(ErrorMessage = "Wprowadz ulice")]
        [StringLength(100)]
        public string Ulica { get; set; }

        [Required(ErrorMessage = "Wprowadz kod pocztowy")]
        [StringLength(6)]
        public string KodPocztowy { get; set; }

        public string Email { get; set; }
        public string Telefon { get; set; }
        public DateTime DataDodania { get; set; }
        public StanZamowienia StanZamowienia { get; set; }
        public decimal WartoscZamowienia { get; set; }

        public  List<PozycjaZamowienia> PozycjeZamowienia { get; set; }
    }
    public enum StanZamowienia
    {
        Nowe,
        Zrealizowane
    }
}