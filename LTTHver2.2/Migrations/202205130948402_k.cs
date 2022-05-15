namespace LTTHver2._2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class k : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(maxLength: 250),
                        Alias = c.String(maxLength: 250, unicode: false),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        CreatedDate = c.Long(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 50, unicode: false),
                        UpdatedDate = c.Long(),
                        UpdatedBy = c.Int(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(maxLength: 250),
                        Alias = c.String(maxLength: 250, unicode: false),
                        Image = c.String(maxLength: 250, unicode: false),
                        CategoryID = c.Int(),
                        Description = c.String(),
                        Content = c.String(),
                        CreatedDate = c.Long(),
                        CreatedBy = c.Int(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 250),
                        HotFlag = c.Boolean(),
                        ViewCount = c.Int(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.BlogCategories", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        BlogID = c.Int(),
                        DisplayOrder = c.Int(),
                        ParentID = c.Int(),
                        Content = c.String(),
                        CreatedDate = c.Long(),
                        CreatedBy = c.String(maxLength: 250, unicode: false),
                        UpdatedDate = c.Long(),
                        UpdatedBy = c.String(maxLength: 250, unicode: false),
                        Status = c.Int(),
                        UserID = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Blogs", t => t.BlogID)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.BlogID)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false, maxLength: 20, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Address = c.String(maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        City = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                        Description = c.String(),
                        Image = c.String(unicode: false),
                        BirthDay = c.Long(),
                        Career = c.String(maxLength: 100),
                        CreatedDay = c.Long(nullable: false),
                        Level = c.Int(nullable: false),
                        IDChucVu = c.Int(nullable: false),
                        Wallet = c.Int(),
                        RequiresVerification = c.Boolean(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(maxLength: 128, fixedLength: true),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleName", t => t.IDChucVu, cascadeDelete: true)
                .Index(t => t.IDChucVu);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        CreatedTime = c.DateTime(),
                        CreatedByUserID = c.String(maxLength: 128),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.CreatedByUserID)
                .Index(t => t.CreatedByUserID);
            
            CreateTable(
                "dbo.CartComboDetails",
                c => new
                    {
                        CartID = c.Int(nullable: false),
                        ComboID = c.Int(nullable: false),
                        Quantity = c.Int(),
                    })
                .PrimaryKey(t => new { t.CartID, t.ComboID })
                .ForeignKey("dbo.Combos", t => t.ComboID)
                .ForeignKey("dbo.Carts", t => t.CartID)
                .Index(t => t.CartID)
                .Index(t => t.ComboID);
            
            CreateTable(
                "dbo.Combos",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(maxLength: 250),
                        Alias = c.String(maxLength: 250, unicode: false),
                        Status = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 250, unicode: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 250, unicode: false),
                        OriginPrice = c.Int(),
                        PromotionPrice = c.Int(),
                        Image = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ComboDetails",
                c => new
                    {
                        ComboID = c.Int(nullable: false),
                        FoodOptionID = c.Int(nullable: false),
                        Quantity = c.Int(),
                    })
                .PrimaryKey(t => new { t.ComboID, t.FoodOptionID })
                .ForeignKey("dbo.FoodOptions", t => t.FoodOptionID)
                .ForeignKey("dbo.Combos", t => t.ComboID)
                .Index(t => t.ComboID)
                .Index(t => t.FoodOptionID);
            
            CreateTable(
                "dbo.FoodOptions",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        FoodID = c.Int(),
                        Size = c.String(maxLength: 50),
                        Topping = c.String(maxLength: 250),
                        BoundingPrice = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Foods", t => t.FoodID)
                .Index(t => t.FoodID);
            
            CreateTable(
                "dbo.CartFoodDetails",
                c => new
                    {
                        CartID = c.Int(nullable: false),
                        FoodOptionID = c.Int(nullable: false),
                        Quantity = c.Int(),
                    })
                .PrimaryKey(t => new { t.CartID, t.FoodOptionID })
                .ForeignKey("dbo.FoodOptions", t => t.FoodOptionID)
                .ForeignKey("dbo.Carts", t => t.CartID)
                .Index(t => t.CartID)
                .Index(t => t.FoodOptionID);
            
            CreateTable(
                "dbo.Foods",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(maxLength: 250),
                        Alias = c.String(maxLength: 250, unicode: false),
                        Image = c.String(maxLength: 250, unicode: false),
                        OriginPrice = c.Int(),
                        PromotionPrice = c.Int(),
                        CategoryID = c.Int(),
                        Description = c.String(),
                        Content = c.String(),
                        CreatedDate = c.Long(),
                        CreatedBy = c.String(maxLength: 250, unicode: false),
                        UpdatedDate = c.Long(),
                        UpdatedBy = c.String(maxLength: 250, unicode: false),
                        HotFlag = c.Boolean(),
                        ViewCount = c.Int(),
                        Status = c.Int(),
                        SlideID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FoodCategories", t => t.CategoryID)
                .ForeignKey("dbo.Slides", t => t.SlideID)
                .Index(t => t.CategoryID)
                .Index(t => t.SlideID);
            
            CreateTable(
                "dbo.FoodCategories",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(maxLength: 250),
                        Alias = c.String(maxLength: 250, unicode: false),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        CreatedDate = c.Int(),
                        CreatedBy = c.String(maxLength: 250, unicode: false),
                        UpdatedDate = c.Int(),
                        UpdatedBy = c.String(maxLength: 250, unicode: false),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FoodComments",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        FoodID = c.Int(),
                        DisplayOrder = c.Int(),
                        ParentID = c.Int(),
                        Content = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 250, unicode: false),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 250, unicode: false),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Foods", t => t.FoodID)
                .Index(t => t.FoodID);
            
            CreateTable(
                "dbo.PromotionFoodDetails",
                c => new
                    {
                        PromotionID = c.Int(nullable: false),
                        FoodID = c.Int(nullable: false),
                        PercentReduction = c.Int(),
                        DirectedMoneyReduction = c.Int(),
                    })
                .PrimaryKey(t => new { t.PromotionID, t.FoodID })
                .ForeignKey("dbo.Promotions", t => t.PromotionID)
                .ForeignKey("dbo.Foods", t => t.FoodID)
                .Index(t => t.PromotionID)
                .Index(t => t.FoodID);
            
            CreateTable(
                "dbo.Promotions",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(maxLength: 250),
                        Alias = c.String(maxLength: 250, unicode: false),
                        Image = c.String(maxLength: 250, unicode: false),
                        BlogID = c.Int(),
                        Description = c.String(),
                        ActiveDay = c.Int(),
                        EndDay = c.Int(),
                        Content = c.String(),
                        CreatedDate = c.Long(),
                        CreatedBy = c.String(maxLength: 250, unicode: false),
                        UpdatedDate = c.Long(),
                        UpdatedBy = c.String(maxLength: 250, unicode: false),
                        HotFlag = c.Boolean(),
                        ViewCount = c.Int(),
                        Status = c.Int(),
                        TypeID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PromotionTypes", t => t.TypeID)
                .Index(t => t.TypeID);
            
            CreateTable(
                "dbo.PromotionTypes",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(maxLength: 250),
                        Alias = c.String(maxLength: 250, unicode: false),
                        Description = c.String(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Description = c.String(),
                        DisplayOrder = c.Int(),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SlideDetails",
                c => new
                    {
                        SlideID = c.Int(nullable: false),
                        ImageID = c.Int(nullable: false),
                        DisplayOrder = c.Int(),
                    })
                .PrimaryKey(t => new { t.SlideID, t.ImageID })
                .ForeignKey("dbo.Image", t => t.ImageID)
                .ForeignKey("dbo.Slides", t => t.SlideID)
                .Index(t => t.SlideID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Alias = c.String(maxLength: 250, unicode: false),
                        URL = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        Alias = c.String(maxLength: 50, unicode: false),
                        Type = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderFoodDetails",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        FoodOptionID = c.Int(nullable: false),
                        Quantity = c.Int(),
                    })
                .PrimaryKey(t => new { t.OrderID, t.FoodOptionID })
                .ForeignKey("dbo.Orders", t => t.OrderID)
                .ForeignKey("dbo.FoodOptions", t => t.FoodOptionID)
                .Index(t => t.OrderID)
                .Index(t => t.FoodOptionID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(maxLength: 250),
                        CustomerAddress = c.String(maxLength: 250),
                        CustomerMessage = c.String(),
                        PaymentMethod = c.String(maxLength: 50),
                        CreatedTime = c.Int(),
                        CreatedByUserID = c.String(),
                        Status = c.Int(),
                        IDUser = c.String(maxLength: 128),
                        ToTalPrice = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.IDUser)
                .Index(t => t.IDUser);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.RoleName",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenChucVu = c.String(nullable: false, maxLength: 100),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Credential",
                c => new
                    {
                        UserGroupID = c.Int(nullable: false),
                        RoleID = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.UserGroupID, t.RoleID });
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
            CreateTable(
                "dbo.UserGroup",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.FoodTags",
                c => new
                    {
                        FoodID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FoodID, t.TagID })
                .ForeignKey("dbo.Foods", t => t.FoodID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.FoodID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.BlogTags",
                c => new
                    {
                        BlogID = c.Int(nullable: false),
                        TagID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BlogID, t.TagID })
                .ForeignKey("dbo.Blogs", t => t.BlogID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.BlogID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityRoles", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Role", "Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Blogs", "CategoryID", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.BlogTags", "BlogID", "dbo.Blogs");
            DropForeignKey("dbo.IdentityUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "IDChucVu", "dbo.RoleName");
            DropForeignKey("dbo.Orders", "IDUser", "dbo.Users");
            DropForeignKey("dbo.IdentityUserLogins", "User_Id", "dbo.Users");
            DropForeignKey("dbo.IdentityUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Carts", "CreatedByUserID", "dbo.Users");
            DropForeignKey("dbo.CartFoodDetails", "CartID", "dbo.Carts");
            DropForeignKey("dbo.CartComboDetails", "CartID", "dbo.Carts");
            DropForeignKey("dbo.ComboDetails", "ComboID", "dbo.Combos");
            DropForeignKey("dbo.OrderFoodDetails", "FoodOptionID", "dbo.FoodOptions");
            DropForeignKey("dbo.OrderFoodDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.FoodTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.FoodTags", "FoodID", "dbo.Foods");
            DropForeignKey("dbo.SlideDetails", "SlideID", "dbo.Slides");
            DropForeignKey("dbo.SlideDetails", "ImageID", "dbo.Image");
            DropForeignKey("dbo.Foods", "SlideID", "dbo.Slides");
            DropForeignKey("dbo.PromotionFoodDetails", "FoodID", "dbo.Foods");
            DropForeignKey("dbo.Promotions", "TypeID", "dbo.PromotionTypes");
            DropForeignKey("dbo.PromotionFoodDetails", "PromotionID", "dbo.Promotions");
            DropForeignKey("dbo.FoodOptions", "FoodID", "dbo.Foods");
            DropForeignKey("dbo.FoodComments", "FoodID", "dbo.Foods");
            DropForeignKey("dbo.Foods", "CategoryID", "dbo.FoodCategories");
            DropForeignKey("dbo.ComboDetails", "FoodOptionID", "dbo.FoodOptions");
            DropForeignKey("dbo.CartFoodDetails", "FoodOptionID", "dbo.FoodOptions");
            DropForeignKey("dbo.CartComboDetails", "ComboID", "dbo.Combos");
            DropForeignKey("dbo.BlogComments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.BlogComments", "BlogID", "dbo.Blogs");
            DropIndex("dbo.Role", new[] { "Id" });
            DropIndex("dbo.BlogTags", new[] { "TagID" });
            DropIndex("dbo.BlogTags", new[] { "BlogID" });
            DropIndex("dbo.FoodTags", new[] { "TagID" });
            DropIndex("dbo.FoodTags", new[] { "FoodID" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "UserId" });
            DropIndex("dbo.IdentityUserLogins", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "IDUser" });
            DropIndex("dbo.OrderFoodDetails", new[] { "FoodOptionID" });
            DropIndex("dbo.OrderFoodDetails", new[] { "OrderID" });
            DropIndex("dbo.SlideDetails", new[] { "ImageID" });
            DropIndex("dbo.SlideDetails", new[] { "SlideID" });
            DropIndex("dbo.Promotions", new[] { "TypeID" });
            DropIndex("dbo.PromotionFoodDetails", new[] { "FoodID" });
            DropIndex("dbo.PromotionFoodDetails", new[] { "PromotionID" });
            DropIndex("dbo.FoodComments", new[] { "FoodID" });
            DropIndex("dbo.Foods", new[] { "SlideID" });
            DropIndex("dbo.Foods", new[] { "CategoryID" });
            DropIndex("dbo.CartFoodDetails", new[] { "FoodOptionID" });
            DropIndex("dbo.CartFoodDetails", new[] { "CartID" });
            DropIndex("dbo.FoodOptions", new[] { "FoodID" });
            DropIndex("dbo.ComboDetails", new[] { "FoodOptionID" });
            DropIndex("dbo.ComboDetails", new[] { "ComboID" });
            DropIndex("dbo.CartComboDetails", new[] { "ComboID" });
            DropIndex("dbo.CartComboDetails", new[] { "CartID" });
            DropIndex("dbo.Carts", new[] { "CreatedByUserID" });
            DropIndex("dbo.Users", new[] { "IDChucVu" });
            DropIndex("dbo.BlogComments", new[] { "User_Id" });
            DropIndex("dbo.BlogComments", new[] { "BlogID" });
            DropIndex("dbo.Blogs", new[] { "CategoryID" });
            DropTable("dbo.Role");
            DropTable("dbo.BlogTags");
            DropTable("dbo.FoodTags");
            DropTable("dbo.UserGroup");
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Credential");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.RoleName");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderFoodDetails");
            DropTable("dbo.Tags");
            DropTable("dbo.Image");
            DropTable("dbo.SlideDetails");
            DropTable("dbo.Slides");
            DropTable("dbo.PromotionTypes");
            DropTable("dbo.Promotions");
            DropTable("dbo.PromotionFoodDetails");
            DropTable("dbo.FoodComments");
            DropTable("dbo.FoodCategories");
            DropTable("dbo.Foods");
            DropTable("dbo.CartFoodDetails");
            DropTable("dbo.FoodOptions");
            DropTable("dbo.ComboDetails");
            DropTable("dbo.Combos");
            DropTable("dbo.CartComboDetails");
            DropTable("dbo.Carts");
            DropTable("dbo.Users");
            DropTable("dbo.BlogComments");
            DropTable("dbo.Blogs");
            DropTable("dbo.BlogCategories");
        }
    }
}
