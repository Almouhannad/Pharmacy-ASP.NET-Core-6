using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Interfaces.IRepositories;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(PharmacyDbContext context) : base(context)
        {
        }

        #region Queries
        public IEnumerable<Medicine> GetMedicines(int categoryId)
        {
            try
            {
                var medicinesRepository = new MedicineRepository(_context);
                return medicinesRepository.FindAll(e => e.CategoryId == categoryId);
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region Commands

        #endregion


    }
}
