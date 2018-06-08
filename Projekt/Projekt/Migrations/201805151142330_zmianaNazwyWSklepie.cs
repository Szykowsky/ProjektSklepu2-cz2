namespace Projekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zmianaNazwyWSklepie : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PozycjaZamowienia", "sklep_SklepId", "dbo.Sklep");
            DropIndex("dbo.PozycjaZamowienia", new[] { "sklep_SklepId" });
            RenameColumn(table: "dbo.PozycjaZamowienia", name: "sklep_SklepId", newName: "SklepId");
            AlterColumn("dbo.PozycjaZamowienia", "SklepId", c => c.Int(nullable: false));
            CreateIndex("dbo.PozycjaZamowienia", "SklepId");
            AddForeignKey("dbo.PozycjaZamowienia", "SklepId", "dbo.Sklep", "SklepId", cascadeDelete: true);
            DropColumn("dbo.PozycjaZamowienia", "PrzedmiotId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PozycjaZamowienia", "PrzedmiotId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PozycjaZamowienia", "SklepId", "dbo.Sklep");
            DropIndex("dbo.PozycjaZamowienia", new[] { "SklepId" });
            AlterColumn("dbo.PozycjaZamowienia", "SklepId", c => c.Int());
            RenameColumn(table: "dbo.PozycjaZamowienia", name: "SklepId", newName: "sklep_SklepId");
            CreateIndex("dbo.PozycjaZamowienia", "sklep_SklepId");
            AddForeignKey("dbo.PozycjaZamowienia", "sklep_SklepId", "dbo.Sklep", "SklepId");
        }
    }
}
