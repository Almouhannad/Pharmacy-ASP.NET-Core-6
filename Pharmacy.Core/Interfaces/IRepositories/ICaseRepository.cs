using Pharmacy.Core.Entities.General;

namespace Pharmacy.Core.Interfaces.IRepositories
{
    public interface ICaseRepository : IBaseRepository<Case>
    {

        #region Queries
        public IEnumerable<MedicineCase> GetMedicines(int id);

        #endregion

        #region Commands
        public void AddMedicine(MedicineCase medicine);
        public void EditMedicine(MedicineCase medicine);
        public void DeleteMedicine(int caseId, int medicineId);
        #endregion

    }
}
