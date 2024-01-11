using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Microsoft.AspNetCore.Identity;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Offres.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Offre Offres { get; set; }

        public IList<Offre> Offre { get;set; } = new List<Offre>();

        public async Task OnGetAsync()
        {
            Offre = await _context.Offres.ToListAsync();
        }

        public async Task<IActionResult> OnPostAjouterAuPanierAsync(int id)
        {
            var offre = await _context.Offres.FindAsync(id);

            if (offre == null)
            {
                return NotFound();
            }

            // Récupère l'utilisateur actuel
            var utilisateur = await _userManager.GetUserAsync(User);

            if (utilisateur == null)
            {
                // L'utilisateur n'est pas authentifié
                return Redirect("/Identity/Account/Login");
            }

            // Vérifie si l'utilisateur a déjà un panier
            var panier = await _context.Paniers.FirstOrDefaultAsync(p => p.UtilisateurId == utilisateur.Id);

            if (panier == null)
            {
                // Si l'utilisateur n'a pas de panier, créez-en un nouveau
                panier = new Panier { UtilisateurId = utilisateur.Id };
                _context.Paniers.Add(panier);
                await _context.SaveChangesAsync();
            }

            // Ajoute l'offre au panier
            panier.OffresPanier.Add(offre);
            await _context.SaveChangesAsync();

            return Redirect("/Paniers");
        }
    }
}
