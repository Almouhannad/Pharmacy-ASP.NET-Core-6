using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Interfaces.IRepositories;

namespace Pharmacy.Core.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region Repositories
        public ICaseRepository Cases { get; }
        public ICategoryRepository Categories { get; }
        public IIngredientRepository Ingredients { get; }
        public IMedicineRepository Medicines { get; }
        public IPatientRepository Patients { get; }

        // Note that there is no specific repos for these tables
        public IBaseRepository<MedicineCase> MedicineCases { get; }
        public IBaseRepository<MedicineIngredient> MedicineIngredients { get; }

        #endregion

        public IBaseRepository<T> Set<T>() where T : Base;

        // Return number of affected rows
        public Task<int> Complete();
    }
}
