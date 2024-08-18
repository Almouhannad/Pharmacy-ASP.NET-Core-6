using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Response.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;
using Pharmacy.Core.Interfaces.IServices;
using Pharmacy.Core.Interfaces.IUnitOfWork;

namespace Pharmacy.Application.DomainServices
{
    public class IngredientService
        : BaseService<Ingredient, DisplayIngredient, CreateIngredientRequest, EditIngredientRequest>,
        IIngredientService
    {
        public IngredientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region Queries
        public IEnumerable<DisplayMedicineInIngredient> GetMedicines(int ingredientId)
        {
            try
            {
                var medicines = _unitOfWork.Ingredients.GetMedicines(ingredientId)
                                    .OrderByDescending(e => e.Id);
                var responses = new List<DisplayMedicineInIngredient>();
                foreach (var medicine in medicines)
                {
                    var response = new DisplayMedicineInIngredient();
                    responses.Add(response.GetResponse(medicine));
                }
                return responses;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public DisplayMedicineInIngredient GetMedicineById(int ingredientId, int medicineId)
        {
            try
            {
                return GetMedicines(ingredientId)
                        .Where(e => e.MedicineId == medicineId).First();

            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

        #region Commands

        #endregion

        #region Requests

        #endregion

    }
}
