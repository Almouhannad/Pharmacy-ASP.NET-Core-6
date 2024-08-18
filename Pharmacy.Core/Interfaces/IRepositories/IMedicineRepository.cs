using Pharmacy.Core.Entities.General;

namespace Pharmacy.Core.Interfaces.IRepositories
{
    public interface IMedicineRepository : IBaseRepository<Medicine>
    {

        #region Queries

        public IEnumerable<MedicineIngredient> GetIngredients(int medicineId);
        #endregion

        #region Commands
        public void AddIngredient(MedicineIngredient ingredient);
        public void EditIngredient(MedicineIngredient ingredient);
        public void DeleteIngredient(int medicineId, int ingredientId);
        #endregion
    }
}
