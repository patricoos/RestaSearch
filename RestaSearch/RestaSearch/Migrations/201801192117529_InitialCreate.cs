namespace RestaSearch.Migrations
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
                        NazwaKategorii = c.String(maxLength: 100),
                        OpisKategorii = c.String(),
                        MyProperty = c.String(),
                        Ukryty = c.Boolean(nullable: false),
                        Typ = c.Int(nullable: false),
                        NazwaPlikuIkony = c.String(),
                    })
                .PrimaryKey(t => t.KategoriaId);
            
            CreateTable(
                "dbo.LokalKategoria",
                c => new
                    {
                        LokalKategoriaId = c.Int(nullable: false, identity: true),
                        LokalId = c.Int(nullable: false),
                        KategoriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LokalKategoriaId)
                .ForeignKey("dbo.Kategoria", t => t.KategoriaId, cascadeDelete: true)
                .ForeignKey("dbo.Lokal", t => t.LokalId, cascadeDelete: true)
                .Index(t => t.LokalId)
                .Index(t => t.KategoriaId);
            
            CreateTable(
                "dbo.Lokal",
                c => new
                    {
                        LokalId = c.Int(nullable: false, identity: true),
                        NazwaLokalu = c.String(nullable: false, maxLength: 100),
                        DataDodania = c.DateTime(nullable: false),
                        Opis = c.String(),
                        OpisSkrocony = c.String(),
                        Telefon = c.String(),
                        TelefonRezerwacja = c.String(),
                        Ulica = c.String(),
                        NumerBudynku = c.String(),
                        NumerLokalu = c.String(),
                        KodPocztowy = c.String(),
                        NazwMiejscowosc = c.String(),
                        NazwaPlikuObrazka = c.String(),
                        Ukryty = c.Boolean(nullable: false),
                        Wyswietlenia = c.Int(nullable: false),
                        Lat = c.String(),
                        Long = c.String(),
                        StatusLokalu = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.LokalId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DaneUzytkownika_Imie = c.String(),
                        DaneUzytkownika_Nazwisko = c.String(),
                        DaneUzytkownika_Ulica = c.String(),
                        DaneUzytkownika_NumerBudynku = c.String(),
                        DaneUzytkownika_NumerLokalu = c.String(),
                        DaneUzytkownika_KodPocztowy = c.String(),
                        DaneUzytkownika_Miejscowosc = c.String(),
                        DaneUzytkownika_Telefon = c.String(),
                        DaneUzytkownika_Email = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Lokal", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LokalKategoria", "LokalId", "dbo.Lokal");
            DropForeignKey("dbo.LokalKategoria", "KategoriaId", "dbo.Kategoria");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Lokal", new[] { "UserId" });
            DropIndex("dbo.LokalKategoria", new[] { "KategoriaId" });
            DropIndex("dbo.LokalKategoria", new[] { "LokalId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Lokal");
            DropTable("dbo.LokalKategoria");
            DropTable("dbo.Kategoria");
        }
    }
}
