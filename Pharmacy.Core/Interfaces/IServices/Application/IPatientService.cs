using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Entities.ViewModels.Requests.Cases;
using Pharmacy.Core.Entities.ViewModels.Requests.Medicines;
using Pharmacy.Core.Entities.ViewModels.Requests.Patients;
using Pharmacy.Core.Entities.ViewModels.Responses.Cases;
using Pharmacy.Core.Entities.ViewModels.Responses.Ingredients;
using Pharmacy.Core.Entities.ViewModels.Responses.Medicines;
using Pharmacy.Core.Entities.ViewModels.Responses.Patients;

namespace Pharmacy.Core.Interfaces.IServices
{
    public interface IPatientService
        : IBaseService<Patient, DisplayPatient, CreatePatientRequest, EditPatientRequest>

    {

        #region Queries

        public IEnumerable<DisplayCaseInPatient> GetCases(int patientId);
        public IEnumerable<DisplayMedicineInCase> GetMedicinesInCase(int caseId);
        public IEnumerable<DisplayMedicine> GetAvailableMedicines(int caseId);
        public DisplayMedicineInCase GetMedicineInCaseById(int caseId, int medicineId);

        public IEnumerable<DisplayIngredientInMedicine> GetIngredients(int medicineId);
        public DisplayIngredientInMedicine GetIngredientById(int medicineId, int ingredientId);


        #endregion

        #region Commands

        public Task<Patient> CreatePatientAsync(string firstName, string lastName);

        public Task AddCase(AddCaseToPatientRequest case_);
        public Task EditCase(EditCaseInPatientRequest case_);
        public Task DeleteCase(int caseId);

        public Task AddMedicineToCase(AddMedicineToCaseRequest medicine);
        public Task EditMedicineInCase(EditMedicineInCaseRequest medicine);
        public Task DeleteMedicineFromCase(int caseId, int medicineId);

        #endregion

        #region Requests
        public AddCaseToPatientRequest GetAddCaseRequest(int patientId);
        public EditCaseInPatientRequest GetEditCaseRequest(int patientId, int caseId);
        public DisplayCaseInPatient GetDeleteCaseRequest(int patientId, int caseId);

        public AddMedicineToCaseRequest GetAddMedicineRequest(int caseId);
        public EditMedicineInCaseRequest GetEditMedicineRequest(int caseId, int medicineId);
        public DisplayMedicineInCase GetDeleteMedicineRequest(int caseId, int medicineId);

        #endregion
    }
}
