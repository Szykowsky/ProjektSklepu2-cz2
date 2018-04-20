using Glimpse.AspNet.Tab;
using Projekt.DAL;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.Infrastructure
{
    public class KoszykManager
    {
        private SklepContext db;
        private ISessionManager session;

        public KoszykManager(ISessionManager session, SklepContext db)
        {
            this.session = session;
            this.db = db;
        }

        public List<PozycjaKoszyka> PobierzKoszyk()
        {
            List<PozycjaKoszyka> koszyk;

            if (session.Get<List<PozycjaKoszyka>>(Const.KoszykSessionKlucz) == null)
            {
                koszyk = new List<PozycjaKoszyka>();
            }
            else
            {
                koszyk = session.Get<List<PozycjaKoszyka>>(Const.KoszykSessionKlucz) as List<PozycjaKoszyka>;
            }

            return koszyk;
        }

        public void DodajDoKoszyka(int sklepId)
        {
            var koszyk = PobierzKoszyk();
            var pozycjaKoszyka = koszyk.Find(k => k.sklep.SklepId == sklepId);

            if(pozycjaKoszyka != null)
            {
                pozycjaKoszyka.ilosc++; 
            }
            else
            {
                var kursDoDodania = db.Sklep.Where(k => k.SklepId == sklepId).SingleOrDefault();

                if(kursDoDodania != null)
                {
                    var nowaPozycjaKoszyka = new PozycjaKoszyka()
                    {
                        sklep = kursDoDodania,
                        ilosc = 1,
                        wartosc = kursDoDodania.Cena
                    };

                    koszyk.Add(nowaPozycjaKoszyka);
                }
            }

            session.Set(Const.KoszykSessionKlucz, koszyk);
        }

        public int UsunZKoszyka(int SklepID)
        {
            var koszyk = PobierzKoszyk();
            var pozycjaKoszyka = koszyk.Find(k => k.sklep.SklepId == SklepID);

            if(pozycjaKoszyka != null)
            {
                if (pozycjaKoszyka.ilosc > 1)
                {
                    pozycjaKoszyka.ilosc--;
                    return pozycjaKoszyka.ilosc;
                }
                else
                {
                    koszyk.Remove(pozycjaKoszyka);
                }
            }
            return 0;
        }

        public decimal PobierzWartoscKoszyka()
        {
            var koszyk = PobierzKoszyk();
            return koszyk.Sum(k => (k.ilosc * k.sklep.Cena));
        }

        public int PobierzIloscPozycjiKoszyka()
        {
            var koszyk = PobierzKoszyk();
            int ilosc = koszyk.Sum(k => k.ilosc);
            return ilosc;
        }

        public Zamowienia UtworzZamowienie(Zamowienia noweZamowienia, string userID)
        {
            var koszyk = PobierzKoszyk();
            noweZamowienia.DataDodania = DateTime.Now;
            //noweZamowienia.userId = userID;

            db.Zamowienie.Add(noweZamowienia);

            if(noweZamowienia.PozycjeZamowienia == null)
            {
                noweZamowienia.PozycjeZamowienia = new List<PozycjaZamowienia>();       
            }
            decimal koszykWartosc = 0;

            foreach(var koszykelement in koszyk)
            {
                var nowaPozycjaZamowienia = new PozycjaZamowienia()
                {
                    ZamowienieId = koszykelement.sklep.SklepId,
                    Ilosc = koszykelement.ilosc,
                    CenaZakupu = koszykelement.sklep.Cena

                };
                koszykWartosc += (koszykelement.ilosc * koszykelement.sklep.Cena);
                noweZamowienia.PozycjeZamowienia.Add(nowaPozycjaZamowienia); 
            }

            noweZamowienia.WartoscZamowienia = koszykWartosc;
            db.SaveChanges();

            return noweZamowienia;
        }

        public void PustyKoszyk()
        {
            session.Set<List<PozycjaKoszyka>>(Const.KoszykSessionKlucz, null);

        }
    }
}