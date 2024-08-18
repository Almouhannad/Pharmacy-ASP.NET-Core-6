using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;

namespace Pharmacy.Core.Entities.ViewModels.Responses.Ingredients
{
    public class DisplayIngredientInMedicine : ResponseBase<MedicineIngredient>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Ratio { get; set; }

        #region Model to view
        public int MedicineId { get; set; }
        public int IngredientId { get; set; }
        public override DisplayIngredientInMedicine GetResponse(MedicineIngredient entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var response = new DisplayIngredientInMedicine
            {
                Id = entity.Id,
                MedicineId = entity.MedicineId,
                IngredientId = entity.IngredientId,
                Name = entity.Ingredient?.Name,
                Description = entity.Ingredient?.Description,
                Ratio = entity.Ratio
            };
            return response;
        }
        #endregion

    }
}
