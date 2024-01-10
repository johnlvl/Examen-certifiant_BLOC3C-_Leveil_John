using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Json;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Offres.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;

        public IndexModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Offre Offres { get; set; }

        public IList<Offre> Offre { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Offre = await _context.Offres.ToListAsync();
        }

        public async Task<IActionResult> OnPostAjouterAuPanierAsync(int id)
        {

            // Récupère le panier de l'utilisateur depuis la session
            var panier = HttpContext.Session.GetString("Panier");

            // Désérialise le panier
            var panierDeserialise = panier != null ? JsonSerializer.Deserialize<Panier>(panier) : new Panier();

            // Ajoute l'offre au panier avec la quantité spécifiée
            // panierDeserialise.AjouterOffre(offre, Input.Quantite);

            // Sérialise le panier
            var panierSerialise = JsonSerializer.Serialize(panierDeserialise);

            // Enregistre le panier dans la session
            HttpContext.Session.SetString("Panier", panierSerialise);

            return RedirectToPage("/Panier/Index");
        }
    }
}
