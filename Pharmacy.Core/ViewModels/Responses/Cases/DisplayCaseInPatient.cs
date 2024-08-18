using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;

namespace Pharmacy.Core.Entities.ViewModels.Responses.Cases
{
    public class DisplayCaseInPatient : ResponseBase<Case>
    {
        public string Name { get; set; }

        #region Model to view
        public int PatientId { get; set; }

        public override DisplayCaseInPatient GetResponse(Case entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            var response = new DisplayCaseInPatient
            {
                Id = entity.Id,
                PatientId = entity.PatientId,
                Name = entity.Name
            };
            return response;
        }
        #endregion
    }
}
