using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Cases
{
    public class EditCaseInPatientRequest : EditBase<Case>
    {

        public int PatientId { get; set; }
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s,-.!?;:\/]*$", ErrorMessage = "Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        #region Model to view
        public override EditCaseInPatientRequest GetRequest(Case entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var request = new EditCaseInPatientRequest
            {
                Id = entity.Id,
                PatientId = entity.PatientId,
                Name = entity.Name
            };
            return request;
        }
        #endregion

        #region View to model
        public override Case GetModel()
        {
            var model = base.GetModel();
            model.PatientId = PatientId;
            model.Name = Name;
            return model;
        }
        #endregion
    }
}
