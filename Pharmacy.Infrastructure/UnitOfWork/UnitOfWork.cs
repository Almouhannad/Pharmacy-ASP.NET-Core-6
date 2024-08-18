using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;
using Pharmacy.Core.Interfaces.IRepositories;
using Pharmacy.Core.Interfaces.IUnitOfWork;
using Pharmacy.Infrastructure.Data;
using Pharmacy.Infrastructure.Repositories;

namespace Pharmacy.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PharmacyDbContext _context;

        #region Repositories
        public ICaseRepository Cases { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IIngredientRepository Ingredients { get; private set; }
        public IMedicineRepository Medicines { get; private set; }
        public IPatientRepository Patients { get; private set; }
        public IBaseRepository<MedicineCase> MedicineCases { get; private set; }
        public IBaseRepository<MedicineIngredient> MedicineIngredients { get; private set; }

        #endregion

        public UnitOfWork(PharmacyDbContext context)
        {
            _context = context;

            #region Initialize repositories
            Cases = new CaseRepository(context);
            Categories = new CategoryRepository(context);
            Ingredients = new IngredientRepository(context);
            Medicines = new MedicineRepository(context);
            Patients = new PatientRepository(context);
            MedicineCases = new BaseRepository<MedicineCase>(context);
            MedicineIngredients = new BaseRepository<MedicineIngredient>(context);
            #endregion

        }

        #warning Open-closed principle violation 
        // Better to use factory pattern
        public IBaseRepository<T> Set<T>() where T : Base
        {
            if (typeof(T) == typeof(Case))
                return (IBaseRepository<T>)Cases;

            else if (typeof(T) == typeof(Category))
                return (IBaseRepository<T>)Categories;

            else if (typeof(T) == typeof(Ingredient))
                return (IBaseRepository<T>)Ingredients;

            else if (typeof(T) == typeof(Medicine))
                return (IBaseRepository<T>)Medicines;

            else if (typeof(T) == typeof(Patient))
                return (IBaseRepository<T>)Patients;

            else if (typeof(T) == typeof(MedicineCase))
                return (IBaseRepository<T>)MedicineCases;

            else if (typeof(T) == typeof(MedicineIngredient))
                return (IBaseRepository<T>)MedicineIngredients;

            else
                throw new InvalidOperationException("No such repository");
        }

        public async Task<int> Complete()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new PersistenceException(ex);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
