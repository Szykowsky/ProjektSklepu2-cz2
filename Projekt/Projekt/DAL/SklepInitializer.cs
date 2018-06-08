using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Projekt.Migrations;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Projekt.DAL
{
    public class SklepInitializer : MigrateDatabaseToLatestVersion<SklepContext, Configuration>
    {
        public static void SeedSklepData(SklepContext context)
        {
            var kategorie = new List<Kategoria>
            {
                new Kategoria() {KategoriaId=1, NazwaKategorii = "Koszulki", NazwaPlikuIkony="Koszulka.png", OpisKategorii="Jakis opis"},
                new Kategoria() {KategoriaId=2, NazwaKategorii = "Bluzy", NazwaPlikuIkony="Bluza.png", OpisKategorii="Jakis opis2"},
                new Kategoria() {KategoriaId=3, NazwaKategorii = "Spodnie", NazwaPlikuIkony="Spodnie.png", OpisKategorii="Jakis opis3"},
                new Kategoria() {KategoriaId=4, NazwaKategorii = "Buty", NazwaPlikuIkony="Buty.png", OpisKategorii="Jakis opis4"},
                new Kategoria() {KategoriaId=5, NazwaKategorii = "Kurtki", NazwaPlikuIkony="Kurtka.png", OpisKategorii="Jakis opis5"},
                new Kategoria() {KategoriaId=6, NazwaKategorii = "Dodatki", NazwaPlikuIkony="Czapka.png", OpisKategorii="Jakis opis6"},
            };

            kategorie.ForEach(k => context.Kategorie.AddOrUpdate(k));
            context.SaveChanges();

            var sklep = new List<Sklep>
            {
                new Sklep() {SklepId = 1, bestseller = true, Cena = 99, DataDodania=DateTime.Now, KategorieId = 1, Marka = "Sven",
                NazwaPlikuObrazka = "Koszulka.png", OpisPrzedmiotu = "Koszulki", Tytul = "Koszulka SVEN", },
                new Sklep() {SklepId = 2, bestseller = false, Cena = 199, DataDodania=DateTime.Now, KategorieId = 1, Marka = "Sven",
                NazwaPlikuObrazka = "Koszulka.png", OpisPrzedmiotu = "Koszulki", Tytul = "Koszulka SVEN2", },
                new Sklep() {SklepId=3, bestseller= true, Cena= 269, DataDodania=DateTime.Now, KategorieId = 2,Marka="Bogdan",
                NazwaPlikuObrazka = "Bluza.jpg",OpisPrzedmiotu="Bluzy",Tytul="Bluza Bogdan"},
                new Sklep() {SklepId=4, bestseller= true, Cena= 56, DataDodania=DateTime.Now, KategorieId = 6,Marka="Bogdan",
                NazwaPlikuObrazka = "Czapka.png",OpisPrzedmiotu="Dodatki",Tytul="Czapka"},
                new Sklep() {SklepId=5, bestseller=true, Cena=99, DataDodania=DateTime.Now, KategorieId=3,Marka="Adrian",
                NazwaPlikuObrazka="Spodnie.jpg",OpisPrzedmiotu="Spodnie",Tytul="Spodnie Adrian"},
                new Sklep() {SklepId=6, bestseller=true, Cena=199, DataDodania=DateTime.Now, KategorieId=3,Marka="Paulina",
                NazwaPlikuObrazka="Spodnie2.jpg",OpisPrzedmiotu="Spodnie",Tytul="Spodnie Paulina"},
            };

            sklep.ForEach(k => context.Sklep.AddOrUpdate(k));
            context.SaveChanges();
        }

        public static void SeedUzytkownicy(SklepContext db)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            const string name = "admin@sklep.pl";
            const string password = "P@ssw0rd";
            const string roleName = "Admin";

            var user = UserManager.FindByName(name);
            if(user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, DaneUzytkownika = new DaneUzytkownika() };
                var resul = UserManager.Create(user, password);
            }

            var role = roleManager.FindByName(name);
            if(role == null)
            {
                role = new IdentityRole(roleName);
                var roleResult = roleManager.Create(role);
            }

            var rolesForUser = UserManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = UserManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}