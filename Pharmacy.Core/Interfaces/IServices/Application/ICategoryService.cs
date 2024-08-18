using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Categories;
using Pharmacy.Core.Entities.ViewModels.Responses.Categories;
using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;

namespace Pharmacy.Core.Interfaces.IServices
{
    public interface ICategoryService
        : IBaseService<Category, DisplayCategory, CreateCategoryRequest, EditCategoryRequest>

    {
        #region Queries
        public IEnumerable<DisplayMedicineInCategory> GetMedicines(int categoryId);

        public DisplayMedicineInCategory GetMedicineById(int categoryId, int medicineId);


        #endregion

        #region Commands

        #endregion

        #region Requests

        #endregion
    }
}
