using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Examen_certifiant_BLOC3C__Leveil_John.Services.PanierService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Offres.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;

        public IndexModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Offre> Offres { get; set; }

        public async Task OnGetAsync()
        {
            Offres = await _context.Offres.ToListAsync();
        }

        /// <summary>
        /// Gère la soumission du formulaire pour ajouter une offre au panier.
        /// </summary>
        /// <param name="id">L'ID de l'offre à ajouter au panier.</param>
        /// <returns>
        /// - Retourne une réponse NotFound si l'offre n'existe pas en base.
        /// - Ajoute l'offre au panier dans la session et redirige vers la page "Paniers".
        /// </returns>
        public IActionResult OnPostAjouterAuPanier(int id)
        {
            // Check si l'offre existe en base
            if (!_context.Offres.Any(offre => offre.ID == id))
            {
                return NotFound();
            }

            // Ajoute l'offre au panier dans la session
            HttpContext.Session.AjouterAuPanier(id);

            return Redirect("/Paniers");
        }

    }
}
