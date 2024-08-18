using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Responses.Cases
{
    public class DisplayCase : ResponseBase<Case>
    {
        public string Name { get; set; }

        [Display(Name = "Patient Name")]
        public string PatientFullName { get; set; }

        #region Model to view
        public override DisplayCase GetResponse(Case entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var response = new DisplayCase
            {
                Id = entity.Id,
                Name = entity.Name,
                PatientFullName = entity.Patient.FirstName + " " + entity.Patient.LastName
            };
            return response;
        }
        #endregion

    }
}
