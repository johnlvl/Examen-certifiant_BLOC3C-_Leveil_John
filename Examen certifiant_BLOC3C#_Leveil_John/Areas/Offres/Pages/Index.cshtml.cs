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

        public IndexModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Offre> Offres { get; set; }

        public async Task OnGetAsync()
        {
            Offres = await _context.Offres.ToListAsync();
        }

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
