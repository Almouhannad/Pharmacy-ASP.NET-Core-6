using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.SelectLists;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using System.ComponentModel.DataAnnotations;
namespace Pharmacy.Core.Entities.ViewModels.Requests.Medicines
{
    public class AddMedicineToCaseRequest : AddBase<MedicineCase>
    {
        [Display (Name ="Medicine")]
        [Required (ErrorMessage ="Medicine is required")]
        public int? MedicineId { get; set; }

        [Required]
        public int CaseId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of doses must be greater than 0.")]
        [RegularExpression(@"^-?\d+$", ErrorMessage = "Please enter a valid integer value.")]
        [Display(Name = "Number of doses")]
        public int Times { get; set; }
        public SelectMedicineForCase? MedicineSelectList { get; set; }

        #region Model to view
        public override AddMedicineToCaseRequest GetRequest(int caseId)
        {
            var request = new AddMedicineToCaseRequest
            {
                CaseId = caseId,
                MedicineSelectList = new SelectMedicineForCase()
            };
            return request;

        }
        #endregion

        #region View to model
        public override MedicineCase GetModel()
        {
            var model = base.GetModel();
            model.MedicineId = (int)MedicineId;
            model.CaseId = CaseId;
            model.Times = Times;
            return model;
        }
        #endregion
    }
}
