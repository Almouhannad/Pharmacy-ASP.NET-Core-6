using Microsoft.EntityFrameworkCore;
using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Exceptions;
using Pharmacy.Core.Interfaces.IRepositories;
using Pharmacy.Infrastructure.Data;
using System.Linq.Expressions;
using System.Linq.Expressions;


namespace Pharmacy.Infrastructure.Repositories
{
    public class CaseRepository : BaseRepository<Case>, ICaseRepository
    {
        public CaseRepository(PharmacyDbContext context) : base(context)
        {
        }

        #region Queries

        public override IEnumerable<Case> GetAll()
        {
            try
            {
                return FindAll(e => true, new Expression<Func<Case, object>>[] { e => e.Patient });

            }
            catch (Exception)
            {
                throw;
            }
        }
        public override Case GetById(int id)
        {
            try
            {
                return Find(e => e.Id == id, new Expression<Func<Case, object>>[] { e => e.Patient });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<MedicineCase> GetMedicines(int id)
        {
            try
            {
                return _context.MedicineCases.AsQueryable().Where(e => e.CaseId == id)
                    .Include(e => e.Medicine).ThenInclude(e => e.Category);
            }
            catch (Exception ex)
            {
                throw new NotFoundException(ex);
            }

        }

        #endregion

        #region Commands
        public void AddMedicine(MedicineCase medicine)
        {
            try
            {
                var medicineCaseRepository = new BaseRepository<MedicineCase>(_context);
                medicineCaseRepository.Add(medicine);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void EditMedicine(MedicineCase medicine)
        {
            try
            {
                var medicineCaseRepository = new BaseRepository<MedicineCase>(_context);
                medicineCaseRepository.Update(medicine);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public void DeleteMedicine(int caseId, int medicineId)
        {
            try
            {
                var medicineCaseRepository = new BaseRepository<MedicineCase>(_context);
                var id = medicineCaseRepository.Find(e => e.CaseId == caseId && e.MedicineId == medicineId).Id;
                medicineCaseRepository.Delete(id);
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

    }
}
