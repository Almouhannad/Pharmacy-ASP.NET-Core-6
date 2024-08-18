using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.SelectLists;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;
namespace Pharmacy.Core.Entities.ViewModels.Requests.Medicines
{
    public class EditMedicineInCaseRequest : EditBase<MedicineCase>
    {
        [Display(Name = "Medicine")]
        [Required(ErrorMessage = "Medicine is required")]
        public int MedicineId { get; set; }
        [Required]
        public int CaseId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Number of doses must be greater than 0.")]
        [RegularExpression(@"^-?\d+$", ErrorMessage = "Please enter a valid integer value.")]
        [Display(Name = "Number of doses")]
        public int Times { get; set; }

        public string? MedicineTradeName { get; set; } = null;
        public SelectMedicineForCase? MedicineSelectList { get; set; }

        #region Model to view
        public override EditMedicineInCaseRequest GetRequest(MedicineCase entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var request = new EditMedicineInCaseRequest
            {
                Id = entity.Id,
                MedicineId = entity.MedicineId,
                CaseId = entity.CaseId,
                Times = entity.Times,
                MedicineTradeName = entity.Medicine.TradeName,
                MedicineSelectList = new SelectMedicineForCase()
            };
            return request;
        }
        #endregion

        #region View to model
        public override MedicineCase GetModel()
        {
            var model = base.GetModel();
            model.MedicineId = MedicineId;
            model.CaseId = CaseId;
            model.Times = Times;
            return model;
        }
        #endregion
    }
}
