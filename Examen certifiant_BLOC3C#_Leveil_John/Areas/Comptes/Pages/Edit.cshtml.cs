using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Examen_certifiant_BLOC3C__Leveil_John.Models;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Comptes.Pages
{
    public class EditModel : PageModel
    {
        private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;

        public EditModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context)
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

            var comptes =  await _context.Comptes.FirstOrDefaultAsync(m => m.ID == id);
            if (comptes == null)
            {
                return NotFound();
            }
            Comptes = comptes;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Comptes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComptesExists(Comptes.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ComptesExists(int id)
        {
            return _context.Comptes.Any(e => e.ID == id);
        }
    }
}
