using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class DaneUzytkownika
    {
        public string  Imie { get; set; }

        public string Nazwisko { get; set; }

        public string Adres { get; set; }

        public string Miasto { get; set; }

        public string Telefon { get; set; }

        public string KodPocztowy { get; set; }

        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail")]
        public string Email { get; set; }
    }
}