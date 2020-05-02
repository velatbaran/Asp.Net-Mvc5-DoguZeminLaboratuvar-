namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.About",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Text = c.String(nullable: false),
                        AboutImage = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedUsername = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Addresses = c.String(nullable: false, maxLength: 250),
                        Phone1 = c.String(nullable: false, maxLength: 50),
                        Phone2 = c.String(),
                        Email = c.String(nullable: false, maxLength: 50),
                        Twitter = c.String(maxLength: 50),
                        Instagram = c.String(maxLength: 50),
                        Youtube = c.String(maxLength: 50),
                        Linkedin = c.String(maxLength: 50),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedUsername = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectsDone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Text = c.String(nullable: false),
                        GiveJob = c.String(nullable: false, maxLength: 250),
                        ServicesId = c.Int(nullable: false),
                        ProjectDate = c.DateTime(nullable: false),
                        ProjectUrl = c.String(maxLength: 250),
                        SlideImage1 = c.String(),
                        SlideImage2 = c.String(),
                        SlideImage3 = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedUsername = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServicesId, cascadeDelete: true)
                .Index(t => t.ServicesId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false),
                        ServicesImage = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedUsername = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Text = c.String(nullable: false, maxLength: 250),
                        SlideImage = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedUsername = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Username = c.String(nullable: false, maxLength: 50),
                        Degree = c.String(nullable: false, maxLength: 100),
                        Task = c.String(nullable: false, maxLength: 100),
                        IsAdmin = c.Boolean(nullable: false),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                        ProfilImage = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        ModifiedUsername = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectsDone", "ServicesId", "dbo.Services");
            DropIndex("dbo.ProjectsDone", new[] { "ServicesId" });
            DropTable("dbo.Users");
            DropTable("dbo.Slider");
            DropTable("dbo.Services");
            DropTable("dbo.ProjectsDone");
            DropTable("dbo.Contact");
            DropTable("dbo.About");
        }
    }
}
