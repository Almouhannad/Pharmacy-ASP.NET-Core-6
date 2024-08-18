using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.SelectLists;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Ingredients
{
    public class AddIngredientToMedicineRequest : AddBase<MedicineIngredient>
    {
        [Required (ErrorMessage ="Ingredient is required")]
        [Display(Name = "Ingredient")]
        public int IngredientId { get; set; }
        [Required]
        public int MedicineId { get; set; }
        [Required]
        [RegularExpression(@"^([1-9]\d*(\.\d+)?|0\.\d*[1-9]\d*)$", ErrorMessage = "Ratio must be a positive number.")]
        public decimal Ratio { get; set; }
        public SelectIngredientForMedicine? IngredientSelectList { get; set; }

        #region Model to view
        public override AddIngredientToMedicineRequest GetRequest(int medicineId)
        {
            var request = new AddIngredientToMedicineRequest
            {
                MedicineId = medicineId,
                IngredientSelectList = new SelectIngredientForMedicine()
            };
            return request;
        }
        #endregion

        #region View to model
        public override MedicineIngredient GetModel()
        {
            var model = base.GetModel();
            model.MedicineId = MedicineId;
            model.IngredientId = IngredientId;
            model.Ratio = Ratio;
            return model;
        }
        #endregion
    }
}
