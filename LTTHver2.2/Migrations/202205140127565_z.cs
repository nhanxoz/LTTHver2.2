namespace LTTHver2._2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class z : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserName", c => c.String(unicode: false));
            AlterColumn("dbo.Users", "Email", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 20, unicode: false));
        }
    }
}
