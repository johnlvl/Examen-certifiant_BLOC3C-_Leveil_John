using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Examen_certifiant_BLOC3C__Leveil_John.Models;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Comptes.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;

        public CreateModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Compte Comptes { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Comptes.Add(Comptes);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
