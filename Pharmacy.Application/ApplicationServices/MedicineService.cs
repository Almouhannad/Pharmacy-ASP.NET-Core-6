using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Requests.Medicines;
using Pharmacy.Core.Entities.ViewModels.Response.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Responses.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;
using Pharmacy.Core.Exceptions;
using Pharmacy.Core.Interfaces.IServices;
using Pharmacy.Core.Interfaces.IUnitOfWork;
using System.Linq.Expressions;

namespace Pharmacy.Application.DomainServices
{
    public class MedicineService
        : BaseService<Medicine, DisplayMedicine, CreateMedicineRequest, EditMedicineRequest>,
        IMedicineService
    {
        public MedicineService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region Queries

        public IEnumerable<DisplayIngredientInMedicine> GetIngredients(int medicineId)
        {
            try
            {
                var ingredients = _unitOfWork.Medicines.GetIngredients(medicineId)
                                        .OrderByDescending(e => e.Id);
                var responses = new List<DisplayIngredientInMedicine>();

                foreach (var ingredient in ingredients)
                {
                    var response = new DisplayIngredientInMedicine();
                    responses.Add(response.GetResponse(ingredient));
                }
                return responses;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<DisplayIngredient> GetAvailableIngredients(int medicineId)
        {
            try
            {
                var ingredientService = new IngredientService(_unitOfWork);
                var allIngredients = ingredientService.GetAll()
                    .OrderBy(e => e.Name);

                var takenIngredients = GetIngredients(medicineId);
                return allIngredients.Where(e => !takenIngredients.Select(f => f.IngredientId).Contains(e.Id));

            }
            catch (Exception)
            {

                throw;
            }

        }

        public DisplayIngredientInMedicine GetIngredientById(int medicineId, int ingredientId)
        {
            try
            {
                return GetIngredients(medicineId)
                        .Where(e => e.IngredientId == ingredientId).First();
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

        #region Commands
        public async Task AddIngredient(AddIngredientToMedicineRequest ingredient)
        {
            try
            {
                _unitOfWork.Medicines.AddIngredient(ingredient.GetModel());
                await _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task UpdateIngredient(EditIngredientInMedicineRequest ingredient)
        {
            try
            {
                _unitOfWork.Medicines.EditIngredient(ingredient.GetModel());
                await _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task DeleteIngredient(int medicineId, int ingredientId)
        {
            try
            {
                _unitOfWork.Medicines.DeleteIngredient(medicineId, ingredientId);
                await _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

        #region Requests
        public override CreateMedicineRequest GetCreateRequest()
        {
            try
            {
                var request = base.GetCreateRequest();
                var categoryService = new CategoryService(_unitOfWork);

                request.CategorySelectList.Categories = categoryService.GetAll()
                    .OrderBy(e => e.Name);
                return request;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public override EditMedicineRequest GetEditRequest(int id)
        {
            try
            {
                var request = base.GetEditRequest(id);
                var categoryService = new CategoryService(_unitOfWork);
                request.CategorySelectList.Categories = categoryService.GetAll().OrderBy(e => e.Name);
                return request;
            }
            catch (Exception) { throw; }
        }

        public AddIngredientToMedicineRequest GetAddIngredientRequest(int medicineId)
        {
            try
            {
                var request = new AddIngredientToMedicineRequest();
                request = request.GetRequest(medicineId);
                request.IngredientSelectList.Ingredients = GetAvailableIngredients(medicineId);
                return request;
            }
            catch (Exception) { throw; }
        }
        public EditIngredientInMedicineRequest GetEditIngredientRequest(int medicineId, int ingredientId)
        {
            try
            {
                var medicineIngredient = _unitOfWork.MedicineIngredients.Find(e => e.MedicineId == medicineId && e.IngredientId == ingredientId);
                var request = new EditIngredientInMedicineRequest();
                request = request.GetRequest(medicineIngredient);
                request.IngredientSelectList.Ingredients = GetAvailableIngredients(medicineId);
                return request;
            }
            catch (Exception) { throw; }
        }
        public DisplayIngredientInMedicine GetDeleteIngredientRequest(int medicineId, int ingredientId)
        {
            try
            {
                var request = GetIngredients(medicineId).Where(e => e.IngredientId == ingredientId).First();
                if (request == null)
                {
                    throw new NotFoundException();
                }
                return request;

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
