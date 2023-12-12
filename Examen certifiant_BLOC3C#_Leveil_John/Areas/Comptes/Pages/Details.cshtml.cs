using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Examen_certifiant_BLOC3C__Leveil_John.Models;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Comptes.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;

        public DetailsModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Compte Comptes { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comptes = await _context.Comptes.FirstOrDefaultAsync(m => m.ID == id);
            if (comptes == null)
            {
                return NotFound();
            }
            else
            {
                Comptes = comptes;
            }
            return Page();
        }
    }
}
