using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Projekt.App_Start;
using Projekt.DAL;
using Projekt.Infrastructure;
using Projekt.Models;
using Projekt.View_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Projekt.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private SklepContext db = new SklepContext();

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }

        private ApplicationUserManager _userManager;
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

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        // GET: Manage
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            var name = User.Identity.Name;

            if (TempData["ViewData"] != null)
            {
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (User.IsInRole("Admin"))
                ViewBag.UserIsAdmin = true;
            else
                ViewBag.UserIsAdmin = false;

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return View("Error");
            }

            var model = new ManageCredentialsViewModel
            {
                Message = message,
                DaneUzytkownika = user.DaneUzytkownika
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile([Bind(Prefix = "DaneUzytkownika")]DaneUzytkownika daneUzytkownika)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                user.DaneUzytkownika = daneUzytkownika;
                var result = await UserManager.UpdateAsync(user);

                AddErrors(result);
            }

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Index");
            }

            var message = ManageMessageId.ChangePasswordSuccess;
            return RedirectToAction("Index", new { Message = message });
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("password-error", error);
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }

        public ActionResult ListaZamowien()
        {
            bool IsAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = IsAdmin;

            IEnumerable<Zamowienia> ZamowieniaUrzytkownika;

            if (IsAdmin)
            {
                ZamowieniaUrzytkownika = db.Zamowienie.Include("PozycjeZamowienia").OrderByDescending(o => o.DataDodania).ToArray();
            }
            else
            {
                var userID = User.Identity.GetUserId();
                ZamowieniaUrzytkownika = db.Zamowienie.Where(o => o.UserId == userID).Include("PozycjeZamowienia").OrderByDescending(o => o.DataDodania).ToArray();

            }

            return View(ZamowieniaUrzytkownika);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> ZmianaStanuZamowienia(Zamowienia zamowienia) //nie wiem dlaczego zamowienia to null :<<< 
        {
            var ZamowienieDoModyfikacji = await db.Zamowienie.FindAsync(zamowienia.ZamowieniaId);
            //ZamowienieDoModyfikacji.StanZamowienia = zamowienia.StanZamowienia;
            await db.SaveChangesAsync();

            return View("ListaZamowien");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Dodaj(int? przedmiotId, bool? potwierdzenie)
        {
            Sklep sklep;
            if (przedmiotId.HasValue)
            {
                ViewBag.EditMode = true;
                sklep = db.Sklep.Find(przedmiotId);
            }
            else
            {
                ViewBag.EditMode = false;
                sklep = new Sklep();
            }

            var result = new EditPrzedmiotViewModel();
            result.Kategoria = db.Kategorie.ToList();
            result.Sklep = sklep;
            result.Potiwerdzenie = potwierdzenie;

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Dodaj(EditPrzedmiotViewModel vm, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (ModelState.IsValid)
                {
                    var fileExt = Path.GetExtension(file.FileName);
                    var fileName = Guid.NewGuid() + fileExt;

                    var path = Path.Combine(Server.MapPath(AppConfig.IkonyKategoriiFolderWzgledny), fileName);
                    file.SaveAs(path);

                    vm.Sklep.NazwaPlikuObrazka = fileName;
                    vm.Sklep.DataDodania = DateTime.Now;

                    db.Entry(vm.Sklep).State = EntityState.Added;
                    db.SaveChanges();

                    return RedirectToAction("Dodaj", new { potwierdzenie = true });
                }
                else
                {
                    var kategorie = db.Kategorie.ToList();
                    vm.Kategoria = kategorie;
                    return View(vm);
                }

            }
            else
            {
                ModelState.AddModelError("", "Nie Wskazano Pliku");
                var kategorie = db.Kategorie.ToList();
                vm.Kategoria = kategorie;
                return View(vm);
            }
        }

    }
}