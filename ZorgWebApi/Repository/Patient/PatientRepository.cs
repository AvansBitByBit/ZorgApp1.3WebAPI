using System.Data;
using Dapper;
using ZorgWebApi.Models;
using ZorgWebApi.Repository.Patient;

namespace ZorgWebApi.Repository.Patient
{
   
}public class PatientRepository : IPatientRepository
{
    private readonly IDbConnection _dbConnection;

    public PatientRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<PatientModel>> GetPatients()
    {
        var query = "SELECT * FROM Patient";
        return await _dbConnection.QueryAsync<PatientModel>(query);
    }

    public async Task CreatePatient(PatientModel patient)
    {
        var query = "INSERT INTO Patient (Voornaam, Achternaam, OuderVoogd_ID, TrajectID, ArtsID) VALUES (@Voornaam, @Achternaam, @OuderVoogd_ID, @TrajectID, @ArtsID)";
        await _dbConnection.ExecuteAsync(query, patient);
    }

    public async Task DeletePatient(PatientModel patient)
    {
        var query = "DELETE FROM Patient WHERE ID = @ID";
        await _dbConnection.ExecuteAsync(query, patient);
    }

    public async Task UpdatePatient(PatientModel patient)
    {
        var query = "UPDATE Patient SET Voornaam = @Voornaam, Achternaam = @Achternaam, OuderVoogd_ID = @OuderVoogd_ID, TrajectID = @TrajectID, ArtsID = @ArtsID WHERE ID = @ID";
        await _dbConnection.ExecuteAsync(query, patient);
    }

}
