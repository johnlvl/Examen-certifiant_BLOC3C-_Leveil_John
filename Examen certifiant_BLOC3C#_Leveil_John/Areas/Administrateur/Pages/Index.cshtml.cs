using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Administrateur.Pages;

[Authorize("AdministrateurUniquement")]
public class IndexModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public IndexModel(ApplicationDbContext context)
    {
        _context = context;
    }

    public Dictionary<string, int> CommandesParOffre { get; set; }

    public int TotalCommandes { get; set; }

    public int TotalUtilisateurs { get; set; }

    public IActionResult OnGet()
    {
        GetTotalCommandeParOffre();
        GetTotalCommande();
        GetTotalUtilisateur();
        return Page();
    }

    public Dictionary<string, int> GetTotalCommandeParOffre()
    {
        CommandesParOffre = _context.Reservations
            .Include(r => r.Offre) // Charger l'offre associée
            .GroupBy(r => r.Offre.TypeOffre) // Grouper par type d'offre
            .ToDictionary(g => g.Key, g => g.Count()); // Compter le nombre de commandes par type d'offre

        return CommandesParOffre;
    }

    public int GetTotalCommande()
    {
        TotalCommandes = _context.Reservations.Count();

        return TotalCommandes;
    }

    public int GetTotalUtilisateur()
    {
        TotalUtilisateurs = _context.Users.Count();

        return TotalUtilisateurs;
    }
}
