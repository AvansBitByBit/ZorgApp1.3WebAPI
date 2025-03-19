﻿namespace ZorgWebApi.Models
{
   

   

    public class PatientModel
    {
        public int ID { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }

        public DateOnly GeboorteDatum { get; set; }
        public int OuderVoogd_ID { get; set; }
        public int TrajectID { get; set; }
        public int? ArtsID { get; set; }
        public string? UserId { get; set; }
    }

   

    

    
}