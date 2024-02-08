using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Identity.Pages.Account.Manage;

public class ReservationModel : PageModel
{
    private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;

    public ReservationModel(Data.ApplicationDbContext context)
    {

        _context = context;

    }

    public List<Reservation> Reservations { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        // R�cup�re l'utilisateur actuellement connect�
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // R�cup�re les derni�res r�servations de l'utilisateur
        var reservations = await _context.Reservations
        .Include(r => r.Offre) // Inclu les d�tails de l'offre associ�e � la r�servation
        .Where(r => r.ClientId == userId)
        .OrderByDescending(r => r.ID)
        .ToListAsync();

        // Passe les r�servations � la vue
        Reservations = reservations;

        return Page();
    }
}
