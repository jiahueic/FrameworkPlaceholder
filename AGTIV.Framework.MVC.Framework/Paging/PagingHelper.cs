using AGTIV.Framework.MVC.Framework.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AGTIV.Framework.MVC.Framework.Paging
{
    public static class PagingHelper
    {
        public static PagedList<T> GetPagedList<T>(IQueryable<T> data, PagingRequest paging)
        {
            var pagedList = new PagedList<T>();
            var parameterExp = Expression.Parameter(typeof(T), typeof(T).GetTypeInfo().Name);

            if(paging.Search != null && paging.Search.Count > 0)
            {
                foreach(var item in paging.Search)
                {
                    if(item.Operator == ConstantHelper.Paging.Contains)
                    {
                        Expression searchExp = null;

                        foreach(var field in item.Fields)
                        {
                            Expression propertyExp = Expression.Property(parameterExp, field);
                            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            var someValue = Expression.Constant(item.Key, typeof(string));

                            if(propertyExp.Type == typeof(Guid))
                            {
                                var toString = typeof(object).GetMethod("ToString");
                                propertyExp = Expression.Call(propertyExp, toString);
                            }

                            var containsMethodExp = Expression.Call(propertyExp, method, someValue);

                            if(searchExp == null)
                                searchExp = containsMethodExp;
                            else
                                searchExp = Expression.OrElse(searchExp, containsMethodExp);
                        }

                        data = data.Where(Expression.Lambda<Func<T, bool>>(searchExp, parameterExp));
                    }
                }
            }

            if(paging.Where != null && paging.Where.Count > 0)
            {
                Expression exp = ConstructFilter<T>(parameterExp, paging.Where, ConstantHelper.Paging.And);
                data = data.Where(Expression.Lambda<Func<T, bool>>(exp, parameterExp));
            }

            if(paging.Sorted != null && paging.Sorted.Count > 0)
            {
                // Reverse the order that it is sorted based on input order.
                paging.Sorted.Reverse();

                foreach(var item in paging.Sorted)
                {
                    var command = item.Direction == ConstantHelper.Paging.Ascending ? "OrderBy" : "OrderbyDescending";
                    var type = typeof(T);
                    var property = type.GetProperty(item.Name);
                    var parameter = Expression.Parameter(type, "p");
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                    var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, data.Expression, Expression.Quote(orderByExpression));
                    data = data.Provider.CreateQuery<T>(resultExpression);
                }
            }

            pagedList.TotalCount = data.Count();

            if(paging.Skip != 0)
                data = data.Skip(paging.Skip);

            if(paging.Take != 0)
                data = data.Take(paging.Take);

            pagedList.Result = data.ToList();
            return pagedList;
        }

        private static Expression ConstructFilter<T>(ParameterExpression paramExp, List<WhereFilter> whereFilters, string condition)
        {
            Expression exp = null;

            foreach(var item in whereFilters)
            {
                Expression predicateExp = null;

                if(item.predicates != null)
                {
                    predicateExp = ConstructFilter<T>(paramExp, item.predicates, item.Condition);
                }
                else
                {
                    Expression propertyExp = Expression.Property(paramExp, item.Field);
                    var value = item.value;
                    ConvertTypeForComparison(ref propertyExp, ref value);
                    var constant = Expression.Constant(value);
                    MemberExpression memberExp = Expression.MakeMemberAccess(paramExp, typeof(T).GetProperty(item.Field));

                    switch(item.Operator)
                    {
                        case ConstantHelper.Paging.Equal:
                            predicateExp = Expression.Equal(propertyExp, constant);
                            break;
                        case ConstantHelper.Paging.NotEqual:
                            predicateExp = Expression.NotEqual(propertyExp, constant);
                            break;
                        case ConstantHelper.Paging.StartsWith:
                            MethodInfo startsWith = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
                            predicateExp = Expression.Call(memberExp, startsWith, constant);
                            break;
                        case ConstantHelper.Paging.EndsWith:
                            MethodInfo endsWith = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
                            predicateExp = Expression.Call(memberExp, endsWith, constant);
                            break;
                        case ConstantHelper.Paging.Contains:
                            MethodInfo contains = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                            predicateExp = Expression.Call(memberExp, contains, constant);
                            break;
                        default:
                            break;
                    }
                }

                if(exp == null)
                {
                    exp = predicateExp;
                }
                else
                {
                    if(condition == ConstantHelper.Paging.And)
                        exp = Expression.AndAlso(exp, predicateExp);
                    else
                        exp = Expression.OrElse(exp, predicateExp);
                }
            }

            return exp;
        }

        private static void ConvertTypeForComparison(ref Expression propertyExp, ref object value)
        {
            switch(Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.String:
                    if(propertyExp.Type == typeof(Guid))
                    {
                        var toString = typeof(object).GetMethod("ToString");
                        propertyExp = Expression.Call(propertyExp, toString);
                    }
                    break;
                case TypeCode.Int64:
                    value = Convert.ChangeType(value, TypeCode.Int32);
                    break;
                default:
                    break;
            }
        }
    }
}