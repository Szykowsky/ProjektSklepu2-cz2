using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Projekt.App_Start;
using Projekt.DAL;
using Projekt.Infrastructure;
using Projekt.Models;
using Projekt.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    public class KoszykController : Controller
    {
        private KoszykManager koszykManager;
        private ISessionManager sessionManager { get; set; }
        private SklepContext db = new SklepContext();

        public KoszykController()
        {
            sessionManager = new SessionManager();
            koszykManager = new KoszykManager(sessionManager,db);
        }

        // GET: Koszyk
        public ActionResult Index()
        {
            var pozycjeKoszyka = koszykManager.PobierzKoszyk();
            var cenaCalkowita = koszykManager.PobierzWartoscKoszyka();

            KoszykViewModel koszykVm = new KoszykViewModel()
            {
                PozycjeKoszyka = pozycjeKoszyka,
                CenaCalkowita = cenaCalkowita
            };
            return View(koszykVm);
        }

        public ActionResult DodajDoKoszyka(int id)
        {
            koszykManager.DodajDoKoszyka(id);
            return RedirectToAction("Index");
        }

        public int PobierzIloscElementowKoszyka()
        {
           return  koszykManager.PobierzIloscPozycjiKoszyka();
        }

        public ActionResult UsunZKoszyka(int id)
        {
            koszykManager.UsunZKoszyka(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Zaplac()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var zamowienie = new Zamowienia
                {
                    Imie = user.DaneUzytkownika.Imie,
                    Nazwisko = user.DaneUzytkownika.Nazwisko,
                    Adres = user.DaneUzytkownika.Adres,
                    Miasto = user.DaneUzytkownika.Miasto,
                    KodPocztowy = user.DaneUzytkownika.KodPocztowy,
                    Email = user.DaneUzytkownika.Email,
                    Telefon = user.DaneUzytkownika.Telefon,
                };
                return View(zamowienie);
            }
            else
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Zaplac", "Koszyk") });
            }

        }

        [HttpPost]
        public async Task<ActionResult> Zaplac(Zamowienia zamowienieSzczegoly)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                var newOrder = koszykManager.UtworzZamowienie(zamowienieSzczegoly, userId);

                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.DaneUzytkownika);
                await UserManager.UpdateAsync(user);

                koszykManager.PustyKoszyk();

                return RedirectToAction("PotwierdzenieZamowienia");
            }
            else
            {
                return View(zamowienieSzczegoly);
            }
        }

        public ActionResult PotwierdzenieZamowienia()
        {
            return View();
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }
}