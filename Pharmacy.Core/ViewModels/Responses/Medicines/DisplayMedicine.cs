using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Responses.Medicines
{
    public class DisplayMedicine : ResponseBase<Medicine>
    {
        [Display(Name = "Trade Name")]
        public string TradeName { get; set; }

        [Display(Name = "Scientific Name")]
        public string ScientificName { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Form { get; set; }
        public decimal Dose { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        #region Model to view
        public override DisplayMedicine GetResponse(Medicine entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var response = new DisplayMedicine
            {
                Id = entity.Id,
                TradeName = entity.TradeName,
                ScientificName = entity.ScientificName,
                Company = entity.Company,
                Description = entity.Description,
                Form = entity.Form,
                Dose = entity.Dose,
                CategoryName = entity.Category.Name
            };
            return response;
        }
        #endregion
    }
}
