namespace AGTIV.Framework.MVC.Data.Migrations
{
    using AGTIV.Framework.MVC.Data.Customs;
    using AGTIV.Framework.MVC.Entities.Authentication;
    using AGTIV.Framework.MVC.Framework.Constants;
    using AGTIV.Framework.MVC.Framework.Helper;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Reflection;

    internal sealed class Configuration : DbMigrationsConfiguration<AGTIV.Framework.MVC.Data.Context.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            // Add custom sql generator
            SetSqlGenerator("System.Data.SqlClient", new DefaultValueSqlServerMigrationSqlGenerator());
        }

        protected override void Seed(AGTIV.Framework.MVC.Data.Context.AppDbContext context)
        {
            #region Role Migration
            ConstantHelper.Role role_Col = new ConstantHelper.Role();
            var roles = new List<AppRole>();
            var role_Fields = role_Col.GetType().GetFields(BindingFlags.Public | BindingFlags.Static).Where(f => f.FieldType == typeof(string)).ToList();

            foreach (var field in role_Fields)
            {
                var fieldValue = field.GetValue(null).ToString();
                if (context.Set<AppRole>().Where(c => c.Name.Equals(fieldValue)).Count() < 1)
                {
                    AppRole tempRole = new AppRole
                    {
                        Id = Guid.NewGuid(),
                        Name = fieldValue,
                    };
                    roles.Add(tempRole);
                }
            }

            context.Set<AppRole>().AddRange(roles);
            context.SaveChanges();
            #endregion

        }
    }
}
