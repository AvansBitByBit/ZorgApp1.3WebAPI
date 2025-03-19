using ZorgWebApi.Models;

namespace ZorgWebApi.Repository.Patient
{
    public interface IPatientRepository
    {
        Task<IEnumerable<PatientModel>> GetPatients();
        Task CreatePatient(PatientModel patient);
        Task DeletePatient(PatientModel patient);
        Task UpdatePatient(PatientModel patient);
    }
}
