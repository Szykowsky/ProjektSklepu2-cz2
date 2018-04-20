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
                NazwaPlikuObrazka = "Czapka.png",OpisPrzedmiotu="Dodatki",Tytul="Czapka"}
            };

            sklep.ForEach(k => context.Sklep.AddOrUpdate(k));
            context.SaveChanges();
        }
    }
}