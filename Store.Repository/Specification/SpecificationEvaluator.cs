using Microsoft.EntityFrameworkCore;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification
{
    public class SpecificationEvaluator<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISepcification<TEntity> specs)
        {
            var Query = InputQuery;
            if(specs.Condition is not null)
                Query = Query.Where(specs.Condition);
            if(specs.OrderBy is not null)
                Query = Query.OrderBy(specs.OrderBy);
            if(specs.OrderByDescending is not null)
                Query = Query.OrderByDescending(specs.OrderByDescending);

            if(specs.isPaginated)
                Query = Query.Skip(specs.Skip).Take(specs.Take);
            Query = specs.Includes.Aggregate(Query, (current, includeExpression) => current.Include(includeExpression));
      
            return Query; 
        }
    }
}
