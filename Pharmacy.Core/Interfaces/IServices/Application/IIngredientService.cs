using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Response.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;

namespace Pharmacy.Core.Interfaces.IServices
{
    public interface IIngredientService
        : IBaseService<Ingredient, DisplayIngredient, CreateIngredientRequest, EditIngredientRequest>

    {
        #region Queries
        public IEnumerable<DisplayMedicineInIngredient> GetMedicines(int ingredientId);
        public DisplayMedicineInIngredient GetMedicineById(int ingredientId, int medicineId);

        #endregion

        #region Commands

        #endregion

        #region Requests

        #endregion
    }
}
