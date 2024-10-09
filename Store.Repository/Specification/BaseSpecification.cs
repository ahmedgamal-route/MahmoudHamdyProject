using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specification
{
    public class BaseSpecification<T> : ISepcification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> condition)
        {
            Condition = condition;
        }
        public Expression<Func<T, bool>> Condition { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        protected void AddInclude(Expression<Func<T, object>> IncludeExpression)
           => Includes.Add(IncludeExpression);

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool isPaginated { get; private set; }


        protected void AddorderByDescending(Expression<Func<T, object>> orderByDescending)
           => OrderByDescending = orderByDescending;

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
            => OrderBy = orderByExpression;

        protected void ApplyPagination(int skip, int take)
        {
            Take = take;
            Skip = skip;
            isPaginated = true;
        }
    }
}
