using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Examen_certifiant_BLOC3C__Leveil_John.Services.PaimentService;
using Examen_certifiant_BLOC3C__Leveil_John.Services.PanierService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Paniers.Pages;

public class IndexModel : PageModel
{
    private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;

    private readonly PaimentService _paiementService;

    public IndexModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context, PaimentService paiementService)
    {
        _context = context;
        _paiementService = paiementService;
    }

    [BindProperty]
    public Panier Paniers { get; set; }

    public List<Offre> PanierArticle { get; set; } = new List<Offre>();

    public decimal MontantTotal { get; set; }


    public async Task OnGetAsync()
    {
        // Vérifie si l'utilisateur est authentifié
        if (!User.Identity.IsAuthenticated)
        {
            // Redirige vers la page de connexion si l'utilisateur n'est pas authentifié
            Response.Redirect("/Identity/Account/Login");
            return;
        }

        // Récupère le dictionnaire du panier depuis la session
        var panier = HttpContext.Session.GetPanierArticle();

        // Récupère les offres du panier à partir de la base de données
        var offreSelectionneeIds = panier.Keys.ToList();
        PanierArticle = await _context.Offres.Where(offre => offreSelectionneeIds.Contains(offre.ID)).ToListAsync();

        // Calcule le montant total
        decimal montantTotal = 0;
        foreach (var offre in PanierArticle)
        {
            montantTotal += offre.Prix * panier[offre.ID];
        }

        // Stocke le montant total dans la propriété MontantTotal du Panier
        MontantTotal = montantTotal;
    }



    /// <summary>
    /// Traite la demande de paiement à la soumission du formulaire.
    /// </summary>
    /// <returns>
    /// - Redirige vers la page "Commandes" si le paiement est réussi.
    /// - Gère autrement la situation en cas d'échec de paiement (par exemple, affiche un message d'erreur).
    /// - Redirige vers une autre page si nécessaire.
    /// </returns>
    public IActionResult OnPostProcessPaiement()
    {
        // Appele la méthode ProcessPaiement
        bool paiementReussi = _paiementService.ProcessPaiement(MontantTotal);

        // Si le paiement a réussi, redirige vers la page "Commandes"
        if (paiementReussi)
        {
            return Redirect("/Commandes");
        }

        // Sinon, la gérer d'une autre manière (par exemple, afficher un message d'erreur)
        // ...

        // Si nécessaire, retourne vers une autre page
        return Redirect("/AutrePage");
    }
}
