namespace Examen_certifiant_BLOC3C__Leveil_John.Models
{
    public class Reservation
    {
        public int IDn { get; set; }

        public string StatutPaiement { get; set; }

        public string ClefPaiment { get; set; }

        public int ClientId { get; set; }

        public int OffreId { get; set; }

        public virtual Client Client { get; set; }

        public virtual Offre Offre { get; set; }

    }
}
