namespace LTTHver2._2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class l : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "IDChucVu", "dbo.RoleName");
            DropIndex("dbo.Users", new[] { "IDChucVu" });
            DropTable("dbo.RoleName");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleName",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenChucVu = c.String(nullable: false, maxLength: 100),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Users", "IDChucVu");
            AddForeignKey("dbo.Users", "IDChucVu", "dbo.RoleName", "ID", cascadeDelete: true);
        }
    }
}
