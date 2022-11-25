namespace AGTIV.Framework.MVC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppSecret",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        AppName = c.String(),
                        ClientSecret = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CalendarProfile",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        CalendarProfileId = c.Guid(nullable: false),
                        ParentProfileId = c.Guid(),
                        CalendarProfileName = c.String(),
                        Monday = c.Boolean(nullable: false),
                        Tuesday = c.Boolean(nullable: false),
                        Wednesday = c.Boolean(nullable: false),
                        Thursday = c.Boolean(nullable: false),
                        Friday = c.Boolean(nullable: false),
                        Saturday = c.Boolean(nullable: false),
                        Sunday = c.Boolean(nullable: false),
                        IsParentProfile = c.Boolean(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CalendarProfileHoliday",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        HolidayId = c.Guid(nullable: false),
                        CalendarProfileId = c.Guid(nullable: false),
                        HolidayName = c.String(),
                        Year = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Days = c.Int(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Elmah_Error",
                c => new
                    {
                        ErrorId = c.Guid(nullable: false),
                        Application = c.String(maxLength: 60),
                        Host = c.String(maxLength: 50),
                        Type = c.String(maxLength: 100),
                        Source = c.String(maxLength: 60),
                        Message = c.String(maxLength: 500),
                        User = c.String(maxLength: 50),
                        StatusCode = c.Int(nullable: false),
                        TimeUtc = c.DateTime(nullable: false),
                        Sequence = c.Int(nullable: false),
                        AllXml = c.String(storeType: "ntext"),
                    })
                .PrimaryKey(t => t.ErrorId);
            
            CreateTable(
                "dbo.Image",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(),
                        Extension = c.String(),
                        ContentType = c.String(),
                        FileBytes = c.Binary(),
                        UserProfileId = c.Guid(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserProfileId, cascadeDelete: true)
                .Index(t => t.UserProfileId);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(),
                        EmailAddress = c.String(),
                        MobileNo = c.String(),
                        NewNRIC = c.String(),
                        Address = c.String(),
                        PostCode = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        Department = c.String(),
                        Manager = c.String(),
                        CalendarProfile_Id = c.Guid(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        CalendarProfile_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.CalendarProfile", t => t.CalendarProfile_Id1)
                .Index(t => t.Id)
                .Index(t => t.CalendarProfile_Id1);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ActivationToken = c.String(),
                        ActivationExpiryTime = c.DateTime(),
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
                        UserId = c.Guid(nullable: false),
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
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.RunningNumber",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Format = c.String(),
                        Group = c.String(),
                        RunningNo = c.Int(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkflowMatrix",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        MatrixName = c.String(),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkflowMatrixStage",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        InternalStageName = c.String(),
                        MatrixId = c.Guid(nullable: false),
                        StageOrder = c.Int(nullable: false),
                        CCOnly = c.Boolean(nullable: false),
                        IsOriginatorActioner = c.Boolean(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkflowMatrix", t => t.MatrixId, cascadeDelete: true)
                .Index(t => t.MatrixId);
            
            CreateTable(
                "dbo.WorkflowStageGroup",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StageId = c.Guid(nullable: false),
                        GroupId = c.Guid(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Group", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.WorkflowMatrixStage", t => t.StageId, cascadeDelete: true)
                .Index(t => t.StageId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.WorkflowStageUser",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        StageId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CreatedBy = c.Guid(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.Guid(nullable: false),
                        ModifiedOn = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.WorkflowMatrixStage", t => t.StageId, cascadeDelete: true)
                .Index(t => t.StageId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AppRoleGroup",
                c => new
                    {
                        AppRole_Id = c.Guid(nullable: false),
                        Group_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppRole_Id, t.Group_Id })
                .ForeignKey("dbo.AspNetRoles", t => t.AppRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.Group", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.AppRole_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.GroupUserProfile",
                c => new
                    {
                        Group_Id = c.Guid(nullable: false),
                        UserProfile_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.UserProfile_Id })
                .ForeignKey("dbo.Group", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserProfile", t => t.UserProfile_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.UserProfile_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkflowStageUser", "StageId", "dbo.WorkflowMatrixStage");
            DropForeignKey("dbo.WorkflowStageUser", "UserId", "dbo.UserProfile");
            DropForeignKey("dbo.WorkflowStageGroup", "StageId", "dbo.WorkflowMatrixStage");
            DropForeignKey("dbo.WorkflowStageGroup", "GroupId", "dbo.Group");
            DropForeignKey("dbo.WorkflowMatrixStage", "MatrixId", "dbo.WorkflowMatrix");
            DropForeignKey("dbo.Image", "UserProfileId", "dbo.UserProfile");
            DropForeignKey("dbo.GroupUserProfile", "UserProfile_Id", "dbo.UserProfile");
            DropForeignKey("dbo.GroupUserProfile", "Group_Id", "dbo.Group");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AppRoleGroup", "Group_Id", "dbo.Group");
            DropForeignKey("dbo.AppRoleGroup", "AppRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserProfile", "CalendarProfile_Id1", "dbo.CalendarProfile");
            DropForeignKey("dbo.UserProfile", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.GroupUserProfile", new[] { "UserProfile_Id" });
            DropIndex("dbo.GroupUserProfile", new[] { "Group_Id" });
            DropIndex("dbo.AppRoleGroup", new[] { "Group_Id" });
            DropIndex("dbo.AppRoleGroup", new[] { "AppRole_Id" });
            DropIndex("dbo.WorkflowStageUser", new[] { "UserId" });
            DropIndex("dbo.WorkflowStageUser", new[] { "StageId" });
            DropIndex("dbo.WorkflowStageGroup", new[] { "GroupId" });
            DropIndex("dbo.WorkflowStageGroup", new[] { "StageId" });
            DropIndex("dbo.WorkflowMatrixStage", new[] { "MatrixId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.UserProfile", new[] { "CalendarProfile_Id1" });
            DropIndex("dbo.UserProfile", new[] { "Id" });
            DropIndex("dbo.Image", new[] { "UserProfileId" });
            DropTable("dbo.GroupUserProfile");
            DropTable("dbo.AppRoleGroup");
            DropTable("dbo.WorkflowStageUser");
            DropTable("dbo.WorkflowStageGroup");
            DropTable("dbo.WorkflowMatrixStage");
            DropTable("dbo.WorkflowMatrix");
            DropTable("dbo.RunningNumber");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Group");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Image");
            DropTable("dbo.Elmah_Error");
            DropTable("dbo.CalendarProfileHoliday");
            DropTable("dbo.CalendarProfile");
            DropTable("dbo.AppSecret");
        }
    }
}
