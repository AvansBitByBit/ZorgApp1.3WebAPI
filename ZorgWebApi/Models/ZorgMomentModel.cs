namespace ZorgWebApi.Models
{
    public class ZorgMomentModel
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public string Url { get; set; }
        public byte[] Plaatje { get; set; }
        public int? TijdsDuurInMin { get; set; }
    }
}
