using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Responses.Medicines
{
    public class DisplayMedicineInCase : ResponseBase<MedicineCase>
    {
        [Display(Name = "Trade Name")]
        public string TradeName { get; set; }

        [Display(Name = "Scientific Name")]
        public string ScientificName { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public string Form { get; set; }
        public decimal? Dose { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Number of doses")]
        public int Times { get; set; }

        #region Model to view

        public int MedicineId { get; set; }
        public int CaseId { get; set; }
        public override DisplayMedicineInCase GetResponse(MedicineCase entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var response = new DisplayMedicineInCase
            {
                Id = entity.Id,
                MedicineId = entity.MedicineId,
                CaseId = entity.CaseId,
                TradeName = entity.Medicine?.TradeName,
                ScientificName = entity.Medicine?.ScientificName,
                Company = entity.Medicine?.Company,
                Description = entity.Medicine?.Description,
                Form = entity.Medicine?.Form,
                Dose = entity.Medicine?.Dose,
                CategoryName = entity.Medicine?.Category?.Name,
                Times = entity.Times
            };
            return response;
        }
        #endregion
    }
}
