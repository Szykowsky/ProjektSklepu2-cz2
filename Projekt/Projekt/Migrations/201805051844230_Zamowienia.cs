namespace Projekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zamowienia : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Zamowienia", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Zamowienia", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            AddColumn("dbo.Zamowienia", "Adres", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Zamowienia", "Komentarz", c => c.String());
            AddColumn("dbo.AspNetUsers", "DaneUzytkownika_KodPocztowy", c => c.String());
            AlterColumn("dbo.Zamowienia", "Email", c => c.String(nullable: false));
            DropColumn("dbo.Zamowienia", "Ulica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Zamowienia", "Ulica", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Zamowienia", "Email", c => c.String());
            DropColumn("dbo.AspNetUsers", "DaneUzytkownika_KodPocztowy");
            DropColumn("dbo.Zamowienia", "Komentarz");
            DropColumn("dbo.Zamowienia", "Adres");
            RenameIndex(table: "dbo.Zamowienia", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Zamowienia", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
