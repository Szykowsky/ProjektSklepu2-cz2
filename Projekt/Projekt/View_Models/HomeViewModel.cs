using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.View_Models
{
    public class HomeViewModel
    {
        public IEnumerable<Kategoria> Kategorie { get; set; }
        public IEnumerable<Sklep> Nowosci { get; set; }
        public IEnumerable<Sklep> Bestsellery { get; set; }

    }
}