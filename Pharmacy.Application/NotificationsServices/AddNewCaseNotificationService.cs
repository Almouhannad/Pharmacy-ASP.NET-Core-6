using Pharmacy.Core.Entities.General.Logs;
using Pharmacy.Core.Entities.ViewModels.Responses.Notifications;
using Pharmacy.Core.Interfaces.IRepositories.Logs;
using Pharmacy.Core.Interfaces.IServices.Notifications;

namespace Pharmacy.Application.NotificationsServices
{
    public class AddNewCaseNotificationService : IAddNewCaseNotificationService
    {
        private readonly IAddNewCaseLogRepository _repository;

        public AddNewCaseNotificationService(IAddNewCaseLogRepository repository)
        {
            _repository = repository;
        }

        #region Queries
        public IEnumerable<DisplayAddNewCaseNotification> GetAll()
        {
            try
            {
                var entities = _repository.GetAll();
                var responses = new List<DisplayAddNewCaseNotification>();
                foreach (var entity in entities)
                {
                    var response = new DisplayAddNewCaseNotification();
                    responses.Add(response.GetResponse(entity));
                }
                return responses;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<DisplayAddNewCaseNotification> GetAllUnread()
        {
            try
            {
                var entities = _repository.GetAllUnread();
                var responses = new List<DisplayAddNewCaseNotification>();
                foreach (var entity in entities)
                {
                    var response = new DisplayAddNewCaseNotification();
                    responses.Add(response.GetResponse(entity));
                }
                return responses;
            }
            catch (Exception) { throw; }
        }
        public int GetUnreadCount()
        {
            try
            {
                return GetAllUnread().Count();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Commands
        public async Task Insert(string patientFirstName, string patientLastName,
            string caseName, DateTime addingDate)
        {
            try
            {
                var addNewCaseLog = new AddNewCaseLog
                {
                    PatientFirstName = patientFirstName,
                    PatientLastName = patientLastName,
                    CaseName = caseName,
                    AddingDate = addingDate,
                    IsRead = false
                };
                await _repository.Insert(addNewCaseLog);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task MarkAllAsRead()
        {
            try
            {
                await _repository.MarkAllAsRead();
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

    }
}
