using Pharmacy.Core.Constants;
using Pharmacy.Core.Entities.General;
using System.Linq.Expressions;

namespace Pharmacy.Core.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : Base
    {
        #region Queries
        public T GetById(int id);
        public IEnumerable<T> GetAll();
        public T Find(Expression<Func<T, bool>> match,
            Expression<Func<T, Object>>[]? includes = null);

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match,
            Expression<Func<T, Object>>[]? includes = null,
            Expression<Func<T, Object>>? orderBy = null, string orderByDirection = OrderByDirection.Ascending);

        #endregion

        #region Commands
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(int id);
        #endregion

    }
}
