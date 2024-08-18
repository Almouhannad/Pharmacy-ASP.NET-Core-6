using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Responses.Medicines
{
    public class DisplayMedicineInCategory : ResponseBase<Medicine>
    {
        [Display(Name = "Trade Name")]
        public string TradeName { get; set; }
        [Display(Name = "Scientific Name")]
        public string ScientificName { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Form { get; set; }
        public decimal Dose { get; set; }
        public int CategoryId { get; set; }

        #region Model to view
        public override DisplayMedicineInCategory GetResponse(Medicine entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var response = new DisplayMedicineInCategory
            {
                Id = entity.Id,
                TradeName = entity.TradeName,
                ScientificName = entity.ScientificName,
                Company = entity.Company,
                Description = entity.Description,
                Form = entity.Form,
                Dose = entity.Dose,
                CategoryId = entity.CategoryId
            };
            return response;
        }
        #endregion
    }
}
