using System;
using System.Linq.Expressions;
using GhostPanel.Core.Data.Model;

namespace GhostPanel.Core.Data.Specifications
{
    public class DataItemPolicy<T> : ISpecification<T> where T : DataEntity
    {
        protected DataItemPolicy(Expression<Func<T, bool>> expression)
        {
            Criteria = expression;
        }

        public static DataItemPolicy<T> All()
        {
            return new DataItemPolicy<T>(x => true);
        }

        public static DataItemPolicy<T> ById(int id)
        {
            return new DataItemPolicy<T>(x => x.Id == id);
        }

        public Expression<Func<T, bool>> Criteria { get; }
    }
}
