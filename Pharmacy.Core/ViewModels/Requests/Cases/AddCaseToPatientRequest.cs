using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Requests.Cases
{
    public class AddCaseToPatientRequest : AddBase<Case>
    {
        public int PatientId { get; set; }
        [RegularExpression(@"^[a-zA-Z][a-zA-Z\s,-.!?;:\/]*$", ErrorMessage = "Name must contain English letters only.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        #region Model to view
        public override AddCaseToPatientRequest GetRequest(int patientId)
        {
            var request = new AddCaseToPatientRequest
            {
                PatientId = patientId
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
