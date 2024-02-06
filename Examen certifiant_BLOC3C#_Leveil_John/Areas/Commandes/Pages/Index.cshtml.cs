using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Examen_certifiant_BLOC3C__Leveil_John.Services.PanierService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Commandes.Pages;

public class IndexModel : PageModel
{
    private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public IndexModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<Offre> PanierArticle { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            // Récupérer l'utilisateur actuellement connecté
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Créer une nouvelle entrée dans la table Panier
            var nouveauPanier = new Panier
            {
                UtilisateurId = userId
            };

            // Ajouter les offres du panier à la collection Offres du nouveau panier
            var panier = HttpContext.Session.GetPanierArticle();
            foreach (var offreId in panier.Keys)
            {
                var offre = await _context.Offres.FindAsync(offreId);
                nouveauPanier.Offres.Add(offre);
            }

            // Enregistrer le nouveau panier avec les offres
            _context.Paniers.Add(nouveauPanier);
            await _context.SaveChangesAsync();

            // Supprimer le panier de la session après avoir passé la commande
            HttpContext.Session.Remove("Panier");

            // Créer les réservations pour chaque offre dans le panier
            foreach (var offreId in panier.Keys)
            {
                var offre = await _context.Offres.FindAsync(offreId);

                // Générer la clé unique pour cette offre
                string uniqueKey = GenerateUniqueKey(offre);

                Reservation reservation = new Reservation();
                reservation.ClefPaiment = uniqueKey;
                reservation.OffreId = offreId;
                reservation.ClientId = userId;

                _context.Reservations.Add(reservation);
            }

            await _context.SaveChangesAsync();

            return Page();
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Erreur lors du paiement. Veuillez réssayer.");
        }
    }


    private string GenerateUniqueKey(Offre offre)
    {
        // Concaténe l'ID de l'offre et le nom de l'offre pour former la clé unique
        string clePaiement = $"{offre.ID}_{offre.TypeOffre}";

        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(clePaiement);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Converti le tableau de bytes en une chaîne hexadécimale
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }

            return sb.ToString();
        }
    }
}