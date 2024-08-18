using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Constants;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;
using Pharmacy.Core.Interfaces.IRepositories;
using Pharmacy.Infrastructure.Data;
using System.Linq.Expressions;

namespace Pharmacy.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        protected readonly PharmacyDbContext _context;
        public BaseRepository(PharmacyDbContext context)
        {
            _context = context;
        }

        #region Queries
        public T Find(Expression<Func<T, bool>> match,
            Expression<Func<T, object>>[] includes = null)
        {
            try
            {
                var query = _context.Set<T>().AsQueryable();
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
                return query.First(match);
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex);
            }

        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> match,
            Expression<Func<T, object>>[] includes = null,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderByDirection.Ascending)
        {
            try
            {
                var query = _context.Set<T>().AsQueryable();
                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                query = query.Where(match);

                if (orderBy != null)
                {
                    if (orderByDirection == OrderByDirection.Ascending)
                    {
                        query = query.OrderBy(orderBy);
                    }
                    else if (orderByDirection == OrderByDirection.Descending)
                    {
                        query = query.OrderByDescending(orderBy);
                    }
                }

                return query.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex);
            }

        }

        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return FindAll(e => true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual T GetById(int id)
        {
            try
            {
                return Find(e => e.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Commands

        public void Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);

            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex);
            }
        }
        public void Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);

            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex);
            }
        }
        public void Delete(int id)
        {
            try
            {
                var entity = GetById(id);
                _context.Set<T>().Remove(entity);
            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex);
            }

        }
        #endregion

    }
}
