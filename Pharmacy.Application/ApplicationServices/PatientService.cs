using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Cases;
using Pharmacy.Core.Entities.ViewModels.Requests.Medicines;
using Pharmacy.Core.Entities.ViewModels.Requests.Patients;
using Pharmacy.Core.Entities.ViewModels.Responses.Cases;
using Pharmacy.Core.Entities.ViewModels.Responses.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;
using Pharmacy.Core.Entities.ViewModels.Responses.Patients;
using Pharmacy.Core.Exceptions;
using Pharmacy.Core.Interfaces.IServices;
using Pharmacy.Core.Interfaces.IUnitOfWork;

namespace Pharmacy.Application.DomainServices
{
    public class PatientService
        : BaseService<Patient, DisplayPatient, CreatePatientRequest, EditPatientRequest>,
        IPatientService
    {
        public PatientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        #region Queries

        public IEnumerable<DisplayCaseInPatient> GetCases(int patientId)
        {
            try
            {
                var cases = _unitOfWork.Cases.FindAll(e => e.PatientId == patientId)
                    .OrderByDescending(e => e.Id);
                var responses = new List<DisplayCaseInPatient>();

                foreach (var case_ in cases)
                {
                    var response = new DisplayCaseInPatient();
                    responses.Add(response.GetResponse(case_));
                }
                return responses;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<DisplayMedicineInCase> GetMedicinesInCase(int caseId)
        {
            try
            {
                var caseService = new CaseService(_unitOfWork);
                return caseService.GetMedicines(caseId)
                    .OrderBy(e => e.TradeName);
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
                var caseService = new CaseService(_unitOfWork);
                return caseService.GetAvailableMedicines(caseId);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public DisplayMedicineInCase GetMedicineInCaseById(int caseId, int medicineId)
        {
            try
            {
                return GetMedicinesInCase(caseId)
                        .Where(e => e.MedicineId == medicineId).First();
            }
            catch (Exception)
            {

                throw;
            }

        }


        public IEnumerable<DisplayIngredientInMedicine> GetIngredients(int medicineId)
        {
            try
            {
                var medicineService = new MedicineService(_unitOfWork);
                return medicineService.GetIngredients(medicineId)
                    .OrderBy(e => e.Name);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public DisplayIngredientInMedicine GetIngredientById(int medicineId, int ingredientId)
        {
            try
            {
                return GetIngredients(medicineId)
                    .Where(e => e.IngredientId == ingredientId).First();
            }
            catch (Exception)
            {

                throw;
            }

        }



        #endregion

        #region Commands
        public async Task<Patient> CreatePatientAsync(string firstName, string lastName)
        {
            try
            {
                var patient = _unitOfWork.Patients.CreatePatientAsync(firstName, lastName);
                await _unitOfWork.Complete();
                return patient;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task AddCase(AddCaseToPatientRequest case_)
        {
            try
            {
                _unitOfWork.Cases.Add(case_.GetModel());
                await _unitOfWork.Complete();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task EditCase(EditCaseInPatientRequest case_)
        {
            try
            {
                _unitOfWork.Cases.Update(case_.GetModel());
                await _unitOfWork.Complete();
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteCase(int caseId)
        {
            try
            {
                var caseService = new CaseService(_unitOfWork);
                await caseService.Delete(caseId);
            }
            catch (Exception) { throw; }
        }

        public async Task AddMedicineToCase(AddMedicineToCaseRequest medicine)
        {
            try
            {
                var caseService = new CaseService(_unitOfWork);
                await caseService.AddMedicine(medicine);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task EditMedicineInCase(EditMedicineInCaseRequest medicine)
        {
            try
            {
                var caseService = new CaseService(_unitOfWork);
                await caseService.UpdateMedicine(medicine);
            }
            catch (Exception) { throw; }
        }

        public async Task DeleteMedicineFromCase(int caseId, int medicineId)
        {
            try
            {
                var caseService = new CaseService(_unitOfWork);
                await caseService.DeleteMedicine(caseId, medicineId);
            }
            catch (Exception) { throw; }
        }

        #endregion

        #region Requests
        public AddCaseToPatientRequest GetAddCaseRequest(int patientId)
        {
            try
            {
                var request = new AddCaseToPatientRequest();
                request = request.GetRequest(patientId);
                return request;
            }
            catch (Exception) { throw; }
        }
        public EditCaseInPatientRequest GetEditCaseRequest(int patientId, int caseId)
        {
            try
            {
                var case_ = _unitOfWork.Cases.GetById(caseId);
                var request = new EditCaseInPatientRequest();
                request = request.GetRequest(case_);
                return request;
            }
            catch (Exception) { throw; }
        }
        public DisplayCaseInPatient GetDeleteCaseRequest(int patientId, int caseId)
        {
            try
            {
                var request = GetCases(patientId).Where(e => e.Id == caseId).First();
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

        public AddMedicineToCaseRequest GetAddMedicineRequest(int caseId)
        {
            try
            {
                var caseService = new CaseService(_unitOfWork);
                return caseService.GetAddMedicineRequest(caseId);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public EditMedicineInCaseRequest GetEditMedicineRequest(int caseId, int medicineId)
        {
            try
            {
                var caseService = new CaseService(_unitOfWork);
                return caseService.GetEditMedicineRequest(caseId, medicineId);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public DisplayMedicineInCase GetDeleteMedicineRequest(int caseId, int medicineId)
        {
            try
            {
                var caseService = new CaseService(_unitOfWork);
                var request =  caseService.GetDeleteMedicineRequest(caseId, medicineId);
                if (request == null)
                {
                    throw new NotFoundException();
                }
                return request;
            }
            catch (Exception) { throw; }
        }

        #endregion


    }
}
