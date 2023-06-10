using Courses.Contract.Models;
using Courses.Data.Models;
using Courses.Web.DTOs;
using System.Linq.Expressions;

namespace Courses.Web.Extensions
{
    public static class QueryExtensions
    {
        //kada korristimo exten metode, one su staticke
        //stavimo this i ona postjae extensions metoda
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, IQueryObj courseQuery, Dictionary<string, Expression<Func<T, object>>> sortColumns)
        {
            if(string.IsNullOrEmpty(courseQuery.SortBy) || !sortColumns.ContainsKey(courseQuery.SortBy))
                return query;
            if (courseQuery.IsSortAsc)
                query = query.OrderBy(sortColumns[courseQuery.SortBy]);
            else
                query = query.OrderByDescending(sortColumns[courseQuery.SortBy]);
            return query;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObj courseQuery)
        {
            if (courseQuery.CurrentPage <= 0)
                courseQuery.CurrentPage = 1;
            if(courseQuery.PageSize <= 0 || courseQuery.PageSize > 250)
                courseQuery.PageSize = 50;
            query = query.Skip((courseQuery.CurrentPage - 1) * courseQuery.PageSize)
                .Take(courseQuery.PageSize);
            return query;

        }


    }
}
