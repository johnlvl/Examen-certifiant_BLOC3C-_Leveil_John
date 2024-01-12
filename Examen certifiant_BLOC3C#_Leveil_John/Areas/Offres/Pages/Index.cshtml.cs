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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Offre> Offres { get; set; }

        public async Task OnGetAsync()
        {
            Offres = await _context.Offres.ToListAsync();
        }

        public IActionResult OnPostAjouterAuPanier(int ID)
        {
            // Récupére le panier de la session
            var panier = _httpContextAccessor.HttpContext.Session.GetPanier();

            // Trouve l'offre dans la base de données
            var offre = _context.Offres.FirstOrDefault(o => o.ID == ID);

            // Vérifie si l'offre a été trouvée
            if (offre == null)
            {
                // L'offre n'est pas trouvée (redirection, message d'erreur, etc.)
                return NotFound();
            }

            // Ajoute l'offre au panier
            panier.OffresPanier.Add(offre);

            // Met à jour le panier dans la session
            _httpContextAccessor.HttpContext.Session.SetPanier(panier);

            return RedirectToPage();
        }

    }
}
