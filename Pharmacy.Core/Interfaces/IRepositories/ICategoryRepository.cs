using Pharmacy.Core.Entities.General;

namespace Pharmacy.Core.Interfaces.IRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        #region Queries
        public IEnumerable<Medicine> GetMedicines(int categoryId);

        #endregion

        #region Commands

        #endregion
    }
}
