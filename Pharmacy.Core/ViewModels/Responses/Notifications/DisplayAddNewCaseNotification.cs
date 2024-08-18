using Pharmacy.Core.Entities.General.Logs;
using Pharmacy.Core.Exceptions;

namespace Pharmacy.Core.Entities.ViewModels.Responses.Notifications
{
    public class DisplayAddNewCaseNotification : ResponseBase<AddNewCaseLog>
    {
        public string Message { get; set; }
        public bool IsRead { get; set; }

        #region Model to view
        public override DisplayAddNewCaseNotification GetResponse(AddNewCaseLog entity)
        {
            if (entity == null)
            {
                throw new NotFoundException();
            }
            return new DisplayAddNewCaseNotification
            {
                Id = entity.Id,
                Message = "Patient " + entity.PatientFirstName + " " + entity.PatientLastName
                + " added case " + entity.CaseName + " in " + entity.AddingDate.ToString(),
                IsRead = entity.IsRead
            };
        }
    }
    #endregion

}
