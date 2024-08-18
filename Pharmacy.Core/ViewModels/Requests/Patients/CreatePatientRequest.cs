using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Patients
{
    public class CreatePatientRequest : CreateBase<Patient>
    {
        [Display(Name = "First Name")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s,-.!?;:\/]*$", ErrorMessage = "First Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s,-.!?;:\/]*$", ErrorMessage = "Last Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        #region Model to view
        public override CreatePatientRequest GetRequest()
        {
            var request = new CreatePatientRequest();
            return request;
        }
        #endregion

        #region View to model
        public override Patient GetModel()
        {
            var model = base.GetModel();
            model.FirstName = FirstName;
            model.LastName = LastName;
            return model;
        }
        #endregion
    }
}
