using System.ComponentModel.DataAnnotations;

namespace Examen_certifiant_BLOC3C__Leveil_John.Models
{
    public class Compte
    {
        public int ID { get; set; }

        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string MotDePasse { get; set; }
    }
}
