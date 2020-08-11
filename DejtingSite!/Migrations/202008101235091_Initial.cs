namespace DatingSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class jaaaa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Friends", "User1Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "User2Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Friends", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "UserPostedId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "UserProfileId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Posts", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "UserReceivedId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "UserSentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FriendRequests", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.Friends", new[] { "User1Id" });
            DropIndex("dbo.Friends", new[] { "User2Id" });
            DropIndex("dbo.Friends", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Friends", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Posts", new[] { "UserPostedId" });
            DropIndex("dbo.Posts", new[] { "UserProfileId" });
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Posts", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.FriendRequests", new[] { "UserSentId" });
            DropIndex("dbo.FriendRequests", new[] { "UserReceivedId" });
            DropIndex("dbo.FriendRequests", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FriendRequests", new[] { "ApplicationUser_Id1" });
            DropTable("dbo.Friends");
            DropTable("dbo.Posts");
            DropTable("dbo.FriendRequests");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.FriendRequests",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserSentId = c.String(maxLength: 128),
                    UserReceivedId = c.String(maxLength: 128),
                    ApplicationUser_Id = c.String(maxLength: 128),
                    ApplicationUser_Id1 = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Posts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserPostedId = c.String(maxLength: 128),
                    UserProfileId = c.String(maxLength: 128),
                    Comment = c.String(),
                    ApplicationUser_Id = c.String(maxLength: 128),
                    ApplicationUser_Id1 = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Friends",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    User1Id = c.String(maxLength: 128),
                    User2Id = c.String(maxLength: 128),
                    ApplicationUser_Id = c.String(maxLength: 128),
                    ApplicationUser_Id1 = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            CreateIndex("dbo.FriendRequests", "ApplicationUser_Id1");
            CreateIndex("dbo.FriendRequests", "ApplicationUser_Id");
            CreateIndex("dbo.FriendRequests", "UserReceivedId");
            CreateIndex("dbo.FriendRequests", "UserSentId");
            CreateIndex("dbo.Posts", "ApplicationUser_Id1");
            CreateIndex("dbo.Posts", "ApplicationUser_Id");
            CreateIndex("dbo.Posts", "UserProfileId");
            CreateIndex("dbo.Posts", "UserPostedId");
            CreateIndex("dbo.Friends", "ApplicationUser_Id1");
            CreateIndex("dbo.Friends", "ApplicationUser_Id");
            CreateIndex("dbo.Friends", "User2Id");
            CreateIndex("dbo.Friends", "User1Id");
            AddForeignKey("dbo.FriendRequests", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FriendRequests", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FriendRequests", "UserSentId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FriendRequests", "UserReceivedId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "UserProfileId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Posts", "UserPostedId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friends", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friends", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friends", "User2Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Friends", "User1Id", "dbo.AspNetUsers", "Id");
        }
    }
}
