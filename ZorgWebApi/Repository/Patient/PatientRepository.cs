using System.Data;
using Dapper;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;

namespace ZorgWebApi.Repository.Patient
{
    /// <summary>
    /// Repository class for managing patient data.
    /// </summary>
    public class PatientRepository : IPatientRepository
    {
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientRepository"/> class.
        /// </summary>
        /// <param name="dbConnection">The database connection to be used by the repository.</param>
        public PatientRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Retrieves all patients from the database.
        /// </summary>
        /// <returns>A collection of <see cref="PatientModel"/>.</returns>
        public async Task<IEnumerable<PatientModel>> GetPatients()
        {
            var query = "SELECT * FROM Patient";
            return await _dbConnection.QueryAsync<PatientModel>(query);
        }

        /// <summary>
        /// Creates a new patient record in the database.
        /// </summary>
        /// <param name="patient">The patient model containing the data to be inserted.</param>
        public async Task CreatePatient(PatientModel patient)
        {
            var query = "INSERT INTO Patient (Voornaam, Achternaam, OuderVoogd_ID, TrajectID, ArtsID) VALUES (@Voornaam, @Achternaam, @OuderVoogd_ID, @TrajectID, @ArtsID)";
            await _dbConnection.ExecuteAsync(query, patient);
        }

        /// <summary>
        /// Deletes an existing patient record from the database.
        /// </summary>
        /// <param name="patient">The patient model containing the ID of the patient to be deleted.</param>
        public async Task DeletePatient(PatientModel patient)
        {
            var query = "DELETE FROM Patient WHERE ID = @ID";
            await _dbConnection.ExecuteAsync(query, patient);
        }

        /// <summary>
        /// Updates an existing patient record in the database.
        /// </summary>
        /// <param name="patient">The patient model containing the updated data.</param>
        public async Task UpdatePatient(PatientModel patient)
        {
            var query = "UPDATE Patient SET Voornaam = @Voornaam, Achternaam = @Achternaam, OuderVoogd_ID = @OuderVoogd_ID, TrajectID = @TrajectID, ArtsID = @ArtsID WHERE ID = @ID";
            await _dbConnection.ExecuteAsync(query, patient);
        }
    }
}
