using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Categories;
using Pharmacy.Core.Entities.ViewModels.Responses.Categories;
using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;
using Pharmacy.Core.Interfaces.IServices;
using Pharmacy.Core.Interfaces.IUnitOfWork;

namespace Pharmacy.Application.DomainServices
{
    public class CategoryService
        : BaseService<Category, DisplayCategory, CreateCategoryRequest, EditCategoryRequest>,
        ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region Queries
        public IEnumerable<DisplayMedicineInCategory> GetMedicines(int categoryId)
        {
            try
            {
                var medicines = _unitOfWork.Categories.GetMedicines(categoryId)
                                    .OrderByDescending(e => e.Id);

                var responses = new List<DisplayMedicineInCategory>();
                foreach (var medicine in medicines)
                {
                    var response = new DisplayMedicineInCategory();
                    responses.Add(response.GetResponse(medicine));
                }
                return responses;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public DisplayMedicineInCategory GetMedicineById(int categoryId, int medicineId)
        {
            try
            {
                return GetMedicines(categoryId)
                    .Where(e => e.Id == medicineId).First();
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
