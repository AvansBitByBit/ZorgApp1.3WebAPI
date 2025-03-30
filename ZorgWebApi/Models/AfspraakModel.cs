
namespace ZorgWebApi.Models
{
    public class AfspraakModel
    {
        public Guid ID { get; set; } // dit was eerst een int,
                                     // als er errors zijn verander het terug,
                                     // maar het hoort een Guid te zijn. xoxo yazandevoet
        public string Titel { get; set; }
        public string NaamDokter { get; set; }
        public string DatumTijd { get; set; }
        public string UserId { get; set; }
        public int Actief { get; set; }
    }
}
