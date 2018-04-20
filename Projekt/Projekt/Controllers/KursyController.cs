using Projekt.DAL;
using Projekt.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class KursyController : Controller
    {
        private SklepContext db = new SklepContext();
        // GET: Kursy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lista(string nazwaKategori)
        {
            var sklep = db.Sklep.Where(a=>a.OpisPrzedmiotu == nazwaKategori).ToList();
            return View(sklep);
        }

        public ActionResult Szczegoly(int id)
        {
            var sklep = db.Sklep.Find(id);
            return View(sklep);
        }

        [ChildActionOnly]
        [OutputCache(Duration =60000)]
        public ActionResult KategorieMenu()
        {
            var kategorie = db.Kategorie.ToList();
            return PartialView("_KategorieMenu", kategorie);
        }

    }
}