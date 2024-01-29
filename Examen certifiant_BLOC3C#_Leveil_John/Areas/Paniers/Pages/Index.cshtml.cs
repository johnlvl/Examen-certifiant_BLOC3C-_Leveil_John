using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Examen_certifiant_BLOC3C__Leveil_John.Models;
using System.Security.Claims;

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
        }
    }
}
