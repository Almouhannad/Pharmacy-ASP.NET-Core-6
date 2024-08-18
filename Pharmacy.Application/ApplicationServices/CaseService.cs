using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Cases;
using Pharmacy.Core.Entities.ViewModels.Requests.Medicines;
using Pharmacy.Core.Entities.ViewModels.Responses.Cases;
using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;
using Pharmacy.Core.Exceptions;
using Pharmacy.Core.Interfaces.IServices;
using Pharmacy.Core.Interfaces.IUnitOfWork;
using System.Linq.Expressions;

namespace Pharmacy.Application.DomainServices
{
    public class CaseService
        : BaseService<Case, DisplayCase, CreateCaseRequest, EditCaseRequest>,
        ICaseService
    {
        public CaseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region Queries

        public IEnumerable<DisplayMedicineInCase> GetMedicines(int caseId)
        {
            try
            {
                var entities = _unitOfWork.Cases.GetMedicines(caseId)
                    .OrderByDescending(e => e.Id);
                var responses = new List<DisplayMedicineInCase>();
                foreach (var entity in entities)
                {
                    var response = new DisplayMedicineInCase();
                    responses.Add(response.GetResponse(entity));
                }
                return responses;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<DisplayMedicine> GetAvailableMedicines(int caseId)
        {
            try
            {
                var medicineService = new MedicineService(_unitOfWork);

                var allMedicines = medicineService.GetAll()
                    .OrderBy(e => e.TradeName);
                var takenMedicines = GetMedicines(caseId);

                return allMedicines.Where(e => !takenMedicines.Select(f => f.MedicineId).Contains(e.Id));
            }
            catch (Exception)
            {

                throw;
            }

        }

        public DisplayMedicineInCase GetMedicineById(int caseId, int medicineId)
        {
            try
            {
                return GetMedicines(caseId)
                    .Where(e => e.MedicineId == medicineId).First();
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

        #region Commands
        public async Task AddMedicine(AddMedicineToCaseRequest medicine)
        {
            try
            {
                _unitOfWork.Cases.AddMedicine(medicine.GetModel());
                await _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task UpdateMedicine(EditMedicineInCaseRequest medicine)
        {
            try
            {
                _unitOfWork.Cases.EditMedicine(medicine.GetModel());
                await _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task DeleteMedicine(int caseId, int medicineId)
        {
            try
            {
                _unitOfWork.Cases.DeleteMedicine(caseId, medicineId);
                await _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region Requests
        public override CreateCaseRequest GetCreateRequest()
        {
            try
            {
                var createRequest = base.GetCreateRequest();
                var patientService = new PatientService(_unitOfWork);
                createRequest.PatientSelectList.Patients = patientService.GetAll()
                    .OrderBy(e => e.FirstName);
                return createRequest;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public override EditCaseRequest GetEditRequest(int id)
        {
            try
            {
                var editRequest = base.GetEditRequest(id);
                var patientService = new PatientService(_unitOfWork);
                editRequest.PatientSelectList.Patients = patientService.GetAll().OrderBy(e => e.FirstName);
                return editRequest;
            }
            catch (Exception) { throw; }
        }

        public AddMedicineToCaseRequest GetAddMedicineRequest(int caseId)
        {
            try
            {
                var request = new AddMedicineToCaseRequest();
                request = request.GetRequest(caseId);
                request.MedicineSelectList.Medicines = GetAvailableMedicines(caseId);
                return request;
            }
            catch (Exception) { throw; }
        }
        public EditMedicineInCaseRequest GetEditMedicineRequest(int caseId, int medicineId)
        {
            try
            {
                var medicineCase = _unitOfWork.MedicineCases.Find(e => e.CaseId == caseId && e.MedicineId == medicineId, new Expression<Func<MedicineCase, object>>[] { e => e.Medicine });
                var request = new EditMedicineInCaseRequest();
                request = request.GetRequest(medicineCase);
                request.MedicineSelectList.Medicines = GetAvailableMedicines(caseId);
                return request;
            }
            catch (Exception) { throw; }
        }
        public DisplayMedicineInCase GetDeleteMedicineRequest(int caseId, int medicineId)
        {
            try
            {
                var request = GetMedicines(caseId).Where(e => e.MedicineId == medicineId).First();
                if (request == null)
                {
                    throw new NotFoundException();
                }
                return request;
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
