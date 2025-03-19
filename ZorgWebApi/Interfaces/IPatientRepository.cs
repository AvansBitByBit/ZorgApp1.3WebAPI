using ZorgWebApi.Models;

namespace ZorgWebApi.Repository.Patient
{
    /// <summary>
    /// Interface for managing patient data.
    /// </summary>
    public interface IPatientRepository
    {
        /// <summary>
        /// Retrieves all patients from the database.
        /// </summary>
        /// <returns>A collection of <see cref="PatientModel"/>.</returns>
        Task<IEnumerable<PatientModel>> GetPatients();

        /// <summary>
        /// Creates a new patient record in the database.
        /// </summary>
        /// <param name="patient">The patient model containing the data to be inserted.</param>
        Task CreatePatient(PatientModel patient);

        /// <summary>
        /// Deletes an existing patient record from the database.
        /// </summary>
        /// <param name="patient">The patient model containing the ID of the patient to be deleted.</param>
        Task DeletePatient(PatientModel patient);

        /// <summary>
        /// Updates an existing patient record in the database.
        /// </summary>
        /// <param name="patient">The patient model containing the updated data.</param>
        Task UpdatePatient(PatientModel patient);
    }
}

