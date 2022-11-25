using AGTIV.Framework.MVC.Entities.Authentication;
using AGTIV.Framework.MVC.Entities.ElmahLog;
using AGTIV.Framework.MVC.Entities.Maintenance;
using AGTIV.Framework.MVC.Entities.Shared;
using AGTIV.Framework.MVC.Entities.User;
using AGTIV.Framework.MVC.Entities.Workflow;
using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.CredentialManager;
using AGTIV.Framework.MVC.Framework.Extensions;
using AGTIV.Framework.MVC.Framework.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace AGTIV.Framework.MVC.Data.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppDbContext()
            : base(ConstantHelper.ConnString.Default)
        {
        }

        #region DbSets
        public DbSet<AppSecret> AppSecret { get; set; }

        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<Group> Group { get; set; }

        public DbSet<CalendarProfile> CalendarProfile { get; set; }

        public DbSet<CalendarProfileHoliday> CalendarProfileHoliday { get; set; }

        public DbSet<Image> File { get; set; }

        public DbSet<RunningNumber> RunningNumber { get; set; }

        #region Workflow
        public DbSet<WorkflowMatrix> WorkflowMatrix { get; set; }
        public DbSet<WorkflowMatrixStage> WorkflowMatrixStage { get; set; }
        public DbSet<WorkflowStageUser> WorkflowStageUser { get; set; }
        public DbSet<WorkflowStageGroup> WorkflowStageGroup { get; set; }
        #endregion Workflow

        public virtual DbSet<Elmah_Error> ElmahError { get; set; }
        public virtual DbSet<i_tblAction> i_tblAction { get; set; }
        public virtual DbSet<i_tblProcess> i_tblProcess { get; set; }
        public virtual DbSet<i_tblTask> i_tblTask { get; set; }
        public virtual DbSet<d_tblStep> d_tblStep { get; set; }

        #endregion

        public int ExtendedSaveChanges()
        {
            var currentDateTime = DateTime.UtcNow;
            var currentUserId = new Guid();

            if (System.Security.Claims.ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                currentUserId = UserAccessControl.GetCurrentUserId();
            }

            var objectStateEntries = ChangeTracker.Entries().Where(
                e => e.Entity != null &&
                e.Entity is IEntity &&
                (e.State == EntityState.Modified || e.State == EntityState.Added))
                .ToList();

            foreach (var entry in objectStateEntries)
            {
                IEntity entityBase = entry.Entity as IEntity;
                if (entityBase == null)
                    continue;

                if (entry.State == EntityState.Added)
                {
                    entityBase.CreatedOn = currentDateTime;
                    entityBase.CreatedBy = currentUserId;
                    entityBase.Status = ConstantHelper.Status.Active;
                }

                entityBase.ModifiedOn = currentDateTime;
                entityBase.ModifiedBy = currentUserId;
            }

            return SaveChanges();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Create tables without adding "s"
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //Add custom SqlDefaultValue attribute
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<SqlDefaultValueAttribute, string>(
                    "SqlDefaultValue",
                    (p, attributes) => attributes.Single().DefaultValue));

            base.OnModelCreating(modelBuilder);
        }

        public static DbContext Create()
        {
            return new AppDbContext();
        }
    }
}
