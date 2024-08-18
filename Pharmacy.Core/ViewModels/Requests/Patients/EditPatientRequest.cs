using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Patients
{
    public class EditPatientRequest : EditBase<Patient>
    {
        [Display(Name = "First Name")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "First Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "Last Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        #region Model to view
        public override EditPatientRequest GetRequest(Patient entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var request = new EditPatientRequest
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
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
