using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Cases;
using Pharmacy.Core.Entities.ViewModels.Requests.Medicines;
using Pharmacy.Core.Entities.ViewModels.Responses.Cases;
using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;

namespace Pharmacy.Core.Interfaces.IServices
{
    public interface ICaseService
        : IBaseService<Case, DisplayCase, CreateCaseRequest, EditCaseRequest>
    {
        #region Queries

        public IEnumerable<DisplayMedicineInCase> GetMedicines(int caseId);
        public IEnumerable<DisplayMedicine> GetAvailableMedicines(int caseId);
        public DisplayMedicineInCase GetMedicineById(int caseId, int medicineId);

        #endregion

        #region Commands

        public Task AddMedicine(AddMedicineToCaseRequest medicine);
        public Task UpdateMedicine(EditMedicineInCaseRequest medicine);
        public Task DeleteMedicine(int caseId, int medicineId);

        #endregion

        #region Requests
        public AddMedicineToCaseRequest GetAddMedicineRequest(int caseId);
        public EditMedicineInCaseRequest GetEditMedicineRequest(int caseId, int medicineId);
        public DisplayMedicineInCase GetDeleteMedicineRequest(int caseId, int medicineId);
        #endregion

    }
}
