using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy.Core.Entities.ViewModels.Responses.Patients
{
    public class DisplayPatient : ResponseBase<Patient>
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        #region Model to view
        public override DisplayPatient GetResponse(Patient entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var response = new DisplayPatient
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
            return response;
        }
        #endregion

    }
}
