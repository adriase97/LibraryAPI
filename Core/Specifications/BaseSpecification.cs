using Core.Interfaces;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : class
    {
        #region Fields
        public Expression<Func<T, bool>>? Criteria { get; }
        #endregion

        #region Constructor
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        #endregion
    }
}
