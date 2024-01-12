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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public Panier Paniers { get; set; }

        public IList<Panier> Panier { get;set; } = default!;

        public async Task OnGetAsync()
        {
            // Récupère l'ID de l'utilisateur actuellement connecté
            var utilisateurId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Récupère le panier de l'utilisateur connecté
            Paniers = await _context.Paniers.Include(p => p.OffresPanier).FirstOrDefaultAsync(p => p.UtilisateurId == utilisateurId);

            // Si le panier n'existe pas, crée un nouveau panier
            if (Paniers == null)
            {
                Paniers = new Panier
                {
                    UtilisateurId = utilisateurId
                };

                _context.Paniers.Add(Paniers);
                await _context.SaveChangesAsync();
            }
        }
    }
}
