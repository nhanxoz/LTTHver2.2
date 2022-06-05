namespace LTTHver2._2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new46 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.FoodComments");
            AlterColumn("dbo.FoodComments", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.FoodComments", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.FoodComments");
            AlterColumn("dbo.FoodComments", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.FoodComments", "ID");
        }
    }
}
