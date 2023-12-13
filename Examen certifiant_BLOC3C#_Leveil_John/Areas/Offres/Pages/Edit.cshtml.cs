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

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Offres.Pages
{
    public class EditModel : PageModel
    {
        private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;

        public EditModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Offre Offre { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offre =  await _context.Offres.FirstOrDefaultAsync(m => m.ID == id);
            if (offre == null)
            {
                return NotFound();
            }
            Offre = offre;
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

            _context.Attach(Offre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OffreExists(Offre.ID))
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

        private bool OffreExists(int id)
        {
            return _context.Offres.Any(e => e.ID == id);
        }
    }
}
