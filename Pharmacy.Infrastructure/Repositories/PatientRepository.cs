using Pharmacy.Core.Entities.General;
using Pharmacy.Core.Interfaces.IRepositories;
using Pharmacy.Infrastructure.Data;

namespace Pharmacy.Infrastructure.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(PharmacyDbContext context) : base(context)
        {
        }

        #region Queries

        #endregion

        #region Commands
        public Patient CreatePatientAsync(string firstName, string lastName)
        {
            try
            {
                var patient = _context.Patients.Add(new Patient
                {
                    Id = 0,
                    FirstName = firstName,
                    LastName = lastName
                });
                return patient.Entity;
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion
    }
}
