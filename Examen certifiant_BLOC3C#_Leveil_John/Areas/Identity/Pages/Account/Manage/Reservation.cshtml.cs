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
        // Récupére l'utilisateur actuellement connecté
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // Récupére les dernières réservations de l'utilisateur
        var reservations = await _context.Reservations
        .Include(r => r.Offre) // Inclu les détails de l'offre associée à la réservation
        .Where(r => r.ClientId == userId)
        .OrderByDescending(r => r.ID)
        .ToListAsync();

        // Passe les réservations à la vue
        Reservations = reservations;

        return Page();
    }
}
