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

    public IndexModel(ApplicationDbContext context, object value, object value1)
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

    /// <summary>
    /// Calcule le total des commandes par type d'offre et retourne un dictionnaire associant chaque type d'offre à son nombre de commandes.
    /// </summary>
    /// <returns>Un dictionnaire contenant le nombre total de commandes par type d'offre.</returns>
    public Dictionary<string, int> GetTotalCommandeParOffre()
    {
        CommandesParOffre = _context.Reservations
            .Include(r => r.Offre) // Charge l'offre associée
            .GroupBy(r => r.Offre.TypeOffre) // Groupe par type d'offre
            .ToDictionary(g => g.Key, g => g.Count()); // Compte le nombre de commandes par type d'offre

        return CommandesParOffre;
    }

    /// <summary>
    /// Calcule le nombre total de commandes enregistrées.
    /// </summary>
    /// <returns>Le nombre total de commandes enregistrées.</returns>
    public int GetTotalCommande()
    {
        TotalCommandes = _context.Reservations.Count();

        return TotalCommandes;
    }

    /// <summary>
    /// Calcule le nombre total d'utilisateurs enregistrés.
    /// </summary>
    /// <returns>Le nombre total d'utilisateurs enregistrés.</returns>
    public int GetTotalUtilisateur()
    {
        TotalUtilisateurs = _context.Users.Count();

        return TotalUtilisateurs;
    }
}
