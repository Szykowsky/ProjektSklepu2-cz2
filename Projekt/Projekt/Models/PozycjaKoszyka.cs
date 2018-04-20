using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class PozycjaKoszyka
    {
        public Sklep sklep { get; set; }
        public int ilosc { get; set; }
        public decimal wartosc { get; set; }
    }
}