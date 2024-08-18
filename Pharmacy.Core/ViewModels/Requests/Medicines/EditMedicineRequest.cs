using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.SelectLists;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Medicines
{
    public class EditMedicineRequest : EditBase<Medicine>
    {
        [Display(Name = "Trade Name")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s,-.!?;:\/]*$", ErrorMessage = "Trade Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Trade Name cannot exceed 50 characters.")]
        public string TradeName { get; set; }

        [Display(Name = "Scientific Name")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s,-.!?;:\/]*$", ErrorMessage = "Scientific Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Scientific Name cannot exceed 50 characters.")]
        public string ScientificName { get; set; }

        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s,-.!?;:\/]*$", ErrorMessage = "Company must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Company cannot exceed 50 characters.")]
        public string Company { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9\s\.,:;!?()\-_]+$",
            ErrorMessage = "Invalid description. Only English letters, numbers, and punctuation marks are allowed.")]
        public string? Description { get; set; } = null;

        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s,-.!?;:\/]*$", ErrorMessage = "Form must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Form cannot exceed 50 characters.")]
        public string Form { get; set; }

        [RegularExpression(@"^([1-9]\d*(\.\d+)?|0\.\d*[1-9]\d*)$", ErrorMessage = "Dose must be a positive number.")]

        public decimal Dose { get; set; }
        [Display(Name = "Category")]
        [Required (ErrorMessage ="Category is required")]
        public int CategoryId { get; set; }
        public SelectCategoryForMedicine? CategorySelectList { get; set; }


        #region Model to view
        public override EditMedicineRequest GetRequest(Medicine entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var request = new EditMedicineRequest
            {
                Id = entity.Id,
                TradeName = entity.TradeName,
                ScientificName = entity.ScientificName,
                Company = entity.Company,
                Description = entity.Description,
                Form = entity.Form,
                Dose = entity.Dose,
                CategoryId = entity.CategoryId,
                CategorySelectList = new SelectCategoryForMedicine()
            };
            return request;
        }
        #endregion

        #region View to model
        public override Medicine GetModel()
        {
            var model = base.GetModel();
            model.TradeName = TradeName;
            model.ScientificName = ScientificName;
            model.Company = Company;
            model.Description = Description;
            model.Form = Form;
            model.Dose = Dose;
            model.CategoryId = CategoryId;
            return model;
        }
        #endregion
    }
}
