using System.Data;
using Dapper;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;

namespace ZorgWebApi.Repository
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
        /// Retrieves all patients for a specific user from the database.
        /// </summary>
        /// <param name="userId">The user ID to filter patients by.</param>
        /// <returns>A collection of <see cref="PatientModel"/>.</returns>
        public async Task<IEnumerable<PatientModel>> GetPatients(string userId)
        {
            var query = "SELECT * FROM Patient WHERE UserId = @UserId";
            return await _dbConnection.QueryAsync<PatientModel>(query, new { UserId = userId });
        }

        /// <summary>
        /// Creates a new patient record in the database.
        /// </summary>
        /// <param name="patient">The patient model containing the data to be inserted.</param>
        public async Task CreatePatient(PatientModel patient)
        {
            var query = "INSERT INTO Patient (Voornaam, Achternaam, OuderVoogd_ID, TrajectID, ArtsID, UserId) VALUES (@Voornaam, @Achternaam, @OuderVoogd_ID, @TrajectID, @ArtsID, @UserId)";
            await _dbConnection.ExecuteAsync(query, patient);
        }

        /// <summary>
        /// Deletes an existing patient record from the database.
        /// </summary>
        /// <param name="patient">The patient model containing the ID and UserId of the patient to be deleted.</param>
        public async Task DeletePatient(PatientModel patient)
        {
            var query = "DELETE FROM Patient WHERE ID = @ID AND UserId = @UserId";
            await _dbConnection.ExecuteAsync(query, patient);
        }

        /// <summary>
        /// Updates an existing patient record in the database.
        /// </summary>
        /// <param name="patient">The patient model containing the updated data.</param>
        public async Task UpdatePatient(PatientModel patient)
        {
            var query = "UPDATE Patient SET Voornaam = @Voornaam, Achternaam = @Achternaam, OuderVoogd_ID = @OuderVoogd_ID, TrajectID = @TrajectID, ArtsID = @ArtsID WHERE ID = @ID AND UserId = @UserId";
            await _dbConnection.ExecuteAsync(query, patient);
        }
    }
}
