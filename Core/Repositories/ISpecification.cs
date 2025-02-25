using System.Linq.Expressions;

namespace Core.Repositories
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
    }
}
