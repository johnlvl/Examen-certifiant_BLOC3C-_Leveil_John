using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Examen_certifiant_BLOC3C__Leveil_John.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string Nom { get; set; }

        [Required]
        [StringLength(100)]
        public string Prenom { get; set; }

        public string? CleCompte { get; set; }
    }
}
