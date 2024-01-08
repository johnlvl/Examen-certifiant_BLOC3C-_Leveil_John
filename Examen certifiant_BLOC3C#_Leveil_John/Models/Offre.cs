using System.ComponentModel.DataAnnotations;

namespace Examen_certifiant_BLOC3C__Leveil_John.Models
{
    public class Offre
    {
        public int ID { get; set; }

        public string TypeOffre { get; set; }

        [DataType(DataType.Currency)]
        public decimal Prix { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
