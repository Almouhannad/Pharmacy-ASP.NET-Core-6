using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Requests.Medicines;
using Pharmacy.Core.Entities.ViewModels.Response.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Responses.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;

namespace Pharmacy.Core.Interfaces.IServices
{
    public interface IMedicineService
        : IBaseService<Medicine, DisplayMedicine, CreateMedicineRequest, EditMedicineRequest>

    {

        #region Queries

        public IEnumerable<DisplayIngredientInMedicine> GetIngredients(int medicineId);
        public IEnumerable<DisplayIngredient> GetAvailableIngredients(int medicineId);
        public DisplayIngredientInMedicine GetIngredientById(int medicineId, int ingredientId);

        #endregion

        #region Commands

        public Task AddIngredient(AddIngredientToMedicineRequest ingredient);
        public Task UpdateIngredient(EditIngredientInMedicineRequest ingredient);
        public Task DeleteIngredient(int medicineId, int ingredientId);

        #endregion

        #region Requests
        public AddIngredientToMedicineRequest GetAddIngredientRequest(int medicineId);
        public EditIngredientInMedicineRequest GetEditIngredientRequest(int medicineId, int ingredientId);
        public DisplayIngredientInMedicine GetDeleteIngredientRequest(int medicineId, int ingredientId);
        #endregion

    }
}
