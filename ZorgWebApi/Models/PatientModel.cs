namespace ZorgWebApi.Models
{
    public class PatientModel
    {
        public int ID { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int TrajectID { get; set; }
        public string? UserId { get; set; }
    }
}