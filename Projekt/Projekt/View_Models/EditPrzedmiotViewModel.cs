using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.View_Models
{
    public class EditPrzedmiotViewModel
    {
        public Sklep Sklep { get; set; }
        public IEnumerable<Kategoria> Kategoria { get; set; }
        public bool? Potiwerdzenie { get; set; }
    }
}