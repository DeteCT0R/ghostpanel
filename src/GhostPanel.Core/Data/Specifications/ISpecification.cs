using System;
using System.Linq.Expressions;

namespace GhostPanel.Core.Data.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}
