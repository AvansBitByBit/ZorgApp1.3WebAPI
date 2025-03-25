namespace ZorgWebApi.Models
{
    public class AfspraakModel
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public string NaamDokter { get; set; }
        public string DatumTijd { get; set; }

        public string UserId { get; set; }
        public int Actief { get; set; }
    }
}
