using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;
using Pharmacy.Core.Interfaces.IRepositories;
using Pharmacy.Infrastructure.Data;
using System.Linq.Expressions;

namespace Pharmacy.Infrastructure.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(PharmacyDbContext context) : base(context)
        {
        }

        #region Queries
        public IEnumerable<MedicineIngredient> GetMedicines(int ingredientId)
        {
            try
            {
                return _context.MedicineIngredients.Where(e => e.IngredientId == ingredientId).AsQueryable()
                    .Include(e => e.Medicine).ThenInclude(e => e.Category);
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex);
            }

        }
        #endregion

        #region Commands

        #endregion

    }
}
