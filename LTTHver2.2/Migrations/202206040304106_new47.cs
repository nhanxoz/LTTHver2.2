namespace LTTHver2._2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new47 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FoodComments", "CreatedBy", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FoodComments", "CreatedBy", c => c.String(maxLength: 250, unicode: false));
        }
    }
}
