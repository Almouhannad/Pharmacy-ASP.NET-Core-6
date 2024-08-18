using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Responses.Medicines
{
    public class DisplayMedicineInIngredient : ResponseBase<MedicineIngredient>
    {
        [Display(Name = "Trade Name")]
        public string TradeName { get; set; }

        [Display(Name = "Scientific Name")]
        public string ScientificName { get; set; }

        public string Company { get; set; }
        public string Description { get; set; }
        public string Form { get; set; }
        public decimal? Dose { get; set; }
        public decimal Ratio { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        #region Model to view
        public int MedicineId { get; set; }
        public int IngredientId { get; set; }
        public override DisplayMedicineInIngredient GetResponse(MedicineIngredient entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var response = new DisplayMedicineInIngredient
            {
                Id = entity.Id,
                MedicineId = entity.MedicineId,
                IngredientId = entity.IngredientId,
                TradeName = entity.Medicine?.TradeName,
                ScientificName = entity.Medicine?.ScientificName,
                Company = entity.Medicine?.Company,
                Description = entity.Medicine?.Description,
                Form = entity.Medicine?.Form,
                Dose = entity.Medicine?.Dose,
                Ratio = entity.Ratio,
                CategoryName = entity.Medicine?.Category?.Name
            };
            return response;
        }
        #endregion
    }
}
