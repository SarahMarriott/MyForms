namespace MyFormBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormName = c.String(),
                        FormLayout = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MyFormSubmissions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserID = c.String(),
                        DateTimeCreated = c.DateTime(nullable: false),
                        SubmittedData = c.String(),
                        MyFormId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MyForms", t => t.MyFormId_Id)
                .Index(t => t.MyFormId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyFormSubmissions", "MyFormId_Id", "dbo.MyForms");
            DropIndex("dbo.MyFormSubmissions", new[] { "MyFormId_Id" });
            DropTable("dbo.MyFormSubmissions");
            DropTable("dbo.MyForms");
        }
    }
}
