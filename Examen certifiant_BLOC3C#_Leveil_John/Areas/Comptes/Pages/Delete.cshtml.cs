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
    public class DeleteModel : PageModel
    {
        private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;

        public DeleteModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comptes = await _context.Comptes.FindAsync(id);
            if (comptes != null)
            {
                Comptes = comptes;
                _context.Comptes.Remove(Comptes);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
