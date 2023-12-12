using System.ComponentModel.DataAnnotations;

namespace Examen_certifiant_BLOC3C__Leveil_John.Models
{
    public class Client
    {
        public int ID { get; set; }

        public string ClefClient { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Prenom { get; set; }

        [Required]
        public int Age { get; set; }

        public int CompteId { get; set; }

        public virtual Compte Compte { get; set; }
    }
}
