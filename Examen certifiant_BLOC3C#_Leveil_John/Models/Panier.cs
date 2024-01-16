namespace Examen_certifiant_BLOC3C__Leveil_John.Models
{
    public class Panier
    {
        public int ID { get; set; }

        public int ClientId { get; set; }

        public string UtilisateurId { get; set; } 

        // Propriété de navigation
        public ICollection<Offre> Offres { get; set; } = new List<Offre>();
    }
}
