namespace Projekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategoria",
                c => new
                    {
                        KategoriaId = c.Int(nullable: false, identity: true),
                        NazwaKategorii = c.String(nullable: false, maxLength: 100),
                        OpisKategorii = c.String(nullable: false),
                        NazwaPlikuIkony = c.String(),
                    })
                .PrimaryKey(t => t.KategoriaId);
            
            CreateTable(
                "dbo.Sklep",
                c => new
                    {
                        SklepId = c.Int(nullable: false, identity: true),
                        KategorieId = c.Int(nullable: false),
                        Tytul = c.String(nullable: false, maxLength: 100),
                        Marka = c.String(nullable: false, maxLength: 100),
                        DataDodania = c.DateTime(nullable: false),
                        NazwaPlikuObrazka = c.String(maxLength: 100),
                        OpisPrzedmiotu = c.String(),
                        Cena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        bestseller = c.Boolean(nullable: false),
                        Ukryty = c.Boolean(nullable: false),
                        Kategoria_KategoriaId = c.Int(),
                    })
                .PrimaryKey(t => t.SklepId)
                .ForeignKey("dbo.Kategoria", t => t.Kategoria_KategoriaId)
                .Index(t => t.Kategoria_KategoriaId);
            
            CreateTable(
                "dbo.PozycjaZamowienia",
                c => new
                    {
                        PozycjaZamowieniaId = c.Int(nullable: false, identity: true),
                        ZamowienieId = c.Int(nullable: false),
                        PrzedmiotId = c.Int(nullable: false),
                        Cena = c.Int(nullable: false),
                        CenaZakupu = c.Decimal(nullable: false, precision: 18, scale: 2),
                        sklep_SklepId = c.Int(),
                        zamowienie_ZamowieniaId = c.Int(),
                    })
                .PrimaryKey(t => t.PozycjaZamowieniaId)
                .ForeignKey("dbo.Sklep", t => t.sklep_SklepId)
                .ForeignKey("dbo.Zamowienia", t => t.zamowienie_ZamowieniaId)
                .Index(t => t.sklep_SklepId)
                .Index(t => t.zamowienie_ZamowieniaId);
            
            CreateTable(
                "dbo.Zamowienia",
                c => new
                    {
                        ZamowieniaId = c.Int(nullable: false, identity: true),
                        Imie = c.String(nullable: false, maxLength: 50),
                        Nazwisko = c.String(nullable: false, maxLength: 100),
                        Miasto = c.String(nullable: false, maxLength: 100),
                        Ulica = c.String(nullable: false, maxLength: 100),
                        KodPocztowy = c.String(nullable: false, maxLength: 6),
                        Email = c.String(),
                        Telefon = c.String(),
                        DataDodania = c.DateTime(nullable: false),
                        StanZamowienia = c.Int(nullable: false),
                        WartoscZamowienia = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ZamowieniaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PozycjaZamowienia", "zamowienie_ZamowieniaId", "dbo.Zamowienia");
            DropForeignKey("dbo.PozycjaZamowienia", "sklep_SklepId", "dbo.Sklep");
            DropForeignKey("dbo.Sklep", "Kategoria_KategoriaId", "dbo.Kategoria");
            DropIndex("dbo.PozycjaZamowienia", new[] { "zamowienie_ZamowieniaId" });
            DropIndex("dbo.PozycjaZamowienia", new[] { "sklep_SklepId" });
            DropIndex("dbo.Sklep", new[] { "Kategoria_KategoriaId" });
            DropTable("dbo.Zamowienia");
            DropTable("dbo.PozycjaZamowienia");
            DropTable("dbo.Sklep");
            DropTable("dbo.Kategoria");
        }
    }
}
