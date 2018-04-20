namespace Projekt.Models
{
    public class PozycjaZamowienia
    {
        public int PozycjaZamowieniaId { get; set; }
        public int ZamowienieId { get; set; }
        public int Ilosc { get; set; }
        public int PrzedmiotId { get; set; }
        public int Cena { get; set; }
        public decimal CenaZakupu { get; set; }

        public virtual Sklep sklep { get; set; }
        public virtual Zamowienia zamowienie { get; set; }
    }
}