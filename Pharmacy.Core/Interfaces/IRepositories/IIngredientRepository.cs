using Pharmacy.Core.Entities.General;

namespace Pharmacy.Core.Interfaces.IRepositories
{
    public interface IIngredientRepository : IBaseRepository<Ingredient>
    {

        #region Queries

        public IEnumerable<MedicineIngredient> GetMedicines(int ingredientId);

        #endregion

        #region Commands

        #endregion
    }
}
