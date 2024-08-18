using Pharmacy.Core.Entities.ViewModels.Responses.Notifications;

namespace Pharmacy.Core.Interfaces.IServices.Notifications
{
    public interface IAddNewCaseNotificationService
    {
        #region Queries
        public IEnumerable<DisplayAddNewCaseNotification> GetAll();
        public IEnumerable<DisplayAddNewCaseNotification> GetAllUnread();
        public int GetUnreadCount();
        #endregion

        #region Commands
        public Task Insert(string patientFirstName, string patientLastName,
            string caseName, DateTime addingDate);
        public Task MarkAllAsRead();
        #endregion
    }
}
