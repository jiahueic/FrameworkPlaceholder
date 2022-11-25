using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using AGTIV.Framework.MVC.Framework.Constants;

namespace AGTIV.Framework.MVC.UI.Web.Extension
{
    public static class MvcHelper
    {
        /// <summary>
        ///     Get display name of model's attribute
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static string GetDisplayName<TModel>(Expression<Func<TModel, object>> expression)
        {
            Type type = typeof(TModel);

            MemberExpression memberExpression = GetMemberExpression<TModel>(expression);
            string propertyName = ((memberExpression.Member is PropertyInfo) ? memberExpression.Member.Name : null);

            // First look into attributes on a type and it's parents
            DisplayAttribute attr;
            attr = (DisplayAttribute)type.GetProperty(propertyName).GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            // Look for [MetadataType] attribute in type hierarchy
            // http://stackoverflow.com/questions/1910532/attribute-isdefined-doesnt-see-attributes-applied-with-metadatatype-class
            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(propertyName);
                    if (property != null)
                    {
                        attr = (DisplayAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();
                    }
                }
            }
            return (attr != null) ? attr.Name : String.Empty;
        }

        public static MemberExpression GetMemberExpression<T>(Expression<Func<T, Object>> expression)
        {
            if (expression.Body is MemberExpression)
            {
                return (MemberExpression)expression.Body;
            }
            else
            {
                var op = ((UnaryExpression)expression.Body).Operand;
                return (MemberExpression)op;
            }
        }

        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static object GetUserDetails(this IPrincipal user, string key)
        {
            var claimValue = ((ClaimsIdentity)user.Identity).GetSpecificClaim(key);
            return claimValue;
        }

        public static string GetUserRole(this IPrincipal user)
        {
            var claimStr = ((ClaimsIdentity)user.Identity).GetSpecificClaim(ConstantHelper.Claims.Roles);
            //int claimValue = int.TryParse(claimStr, out claimValue) ? claimValue : 0;
            //return claimValue;
            return claimStr;
        }
    }
}