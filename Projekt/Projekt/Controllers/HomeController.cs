using Projekt.DAL;
using Projekt.Models;
using Projekt.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class HomeController : Controller
    {
        private SklepContext db = new SklepContext();

        public ActionResult Index()
        {
            var kategorie = db.Kategorie.ToList();

            var nowosci = db.Sklep.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(6).ToList();

            var bestseller = db.Sklep.Where(a => !a.Ukryty && a.bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();

            var vm = new HomeViewModel()
            {
                Kategorie = kategorie,
                Nowosci = nowosci,
                Bestsellery = bestseller

            };
            return View(vm);
        }
  
        public ActionResult StronyStatyczne(string nazwa)
        {
            return View(nazwa);
        }

    }
}