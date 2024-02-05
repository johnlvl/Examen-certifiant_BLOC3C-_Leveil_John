using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Examen_certifiant_BLOC3C__Leveil_John.Services.PanierService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Paniers.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;

        public IndexModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Panier Paniers { get; set; }

        public List<Offre> PanierArticle { get; set; } = new List<Offre>();

        public decimal MontantTotal { get; set; }


        public async Task OnGetAsync()
        {
            // Vérifie si l'utilisateur est authentifié
            if (!User.Identity.IsAuthenticated)
            {
                // Redirige vers la page de connexion si l'utilisateur n'est pas authentifié
                Response.Redirect("/Identity/Account/Login");
                return;
            }

            // Récupère le dictionnaire du panier depuis la session
            var panier = HttpContext.Session.GetPanierArticle();

            // Récupère les offres du panier à partir de la base de données
            var offreSelectionneeIds = panier.Keys.ToList();
            PanierArticle = await _context.Offres.Where(offre => offreSelectionneeIds.Contains(offre.ID)).ToListAsync();

            // Calcule le montant total
            decimal montantTotal = 0;
            foreach (var offre in PanierArticle)
            {
                montantTotal += offre.Prix * panier[offre.ID];
            }

            // Stocke le montant total dans la propriété MontantTotal du Panier
            MontantTotal = montantTotal;
        }
    }
}
