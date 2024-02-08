using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Examen_certifiant_BLOC3C__Leveil_John.Services.PanierService;
using Examen_certifiant_BLOC3C__Leveil_John.Services.QrCodeService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Commandes.Pages;

public class IndexModel : PageModel
{
    private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly QrCodeService _qrCodeService;

    public IndexModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context, UserManager<ApplicationUser> userManager, QrCodeService qrCodeService)
    {
        _context = context;
        _userManager = userManager;
        _qrCodeService = qrCodeService;
    }

    public List<Offre> PanierArticle = new List<Offre>();

    public List<byte[]> QrCodeImages { get; set; }

    /// <summary>
    /// Traite la demande GET pour passer une commande.
    /// </summary>
    /// <returns>
    /// - Redirige vers la page de commande après avoir enregistré les informations de la commande pour afficher le QrCode associé.
    /// - Affiche un message d'erreur si une erreur survient lors du traitement de la demande.
    /// </returns>
    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            // Récupére l'utilisateur actuellement connecté
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.GetUserAsync(User);

            // Crée une nouvelle entrée dans la table Panier
            var nouveauPanier = new Panier
            {
                UtilisateurId = userId
            };

            // Ajoute les offres du panier à la collection Offres du nouveau panier
            var panier = HttpContext.Session.GetPanierArticle();
            foreach (var offreId in panier.Keys)
            {
                var offre = await _context.Offres.FindAsync(offreId);
                nouveauPanier.Offres.Add(offre);

                // Ajoute l'offre à la liste PanierArticle
                PanierArticle.Add(offre);
            }

            // Enregistre le nouveau panier avec les offres
            _context.Paniers.Add(nouveauPanier);
            await _context.SaveChangesAsync();

            // Supprime le panier de la session après avoir passé la commande
            HttpContext.Session.Remove("Panier");

            // Crée une liste pour stocker les images des QR codes
            List<byte[]> qrCodeImages = new List<byte[]>();

            // Crée les réservations pour chaque offre dans le panier
            foreach (var offreId in panier.Keys)
            {
                var offre = await _context.Offres.FindAsync(offreId);

                // Génére la clé unique pour cette offre
                string clePaiement = GenereClePaiement(offre);

                Reservation reservation = new Reservation();
                reservation.ClefPaiment = clePaiement;
                reservation.OffreId = offreId;
                reservation.ClientId = userId;
                reservation.StatutPaiement = "OK";

                if (user != null && !string.IsNullOrEmpty(user.CleCompte))
                {
                    var cleCompte = user.CleCompte;

                    // Génére le QR code et le stocke dans la liste
                    byte[] qrCodeImage = _qrCodeService.GenerateQrCode(cleCompte, clePaiement);
                    qrCodeImages.Add(qrCodeImage);
                    reservation.QrCodeImageData = qrCodeImage;
                }

                _context.Reservations.Add(reservation);
            }
            // Affecte la liste des images des QR codes à la propriété correspondante
            QrCodeImages = qrCodeImages;

            await _context.SaveChangesAsync();

            return Page();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Une erreur s'est produite lors du traitement de votre demande. Veuillez réessayer plus tard.");
            return Page();
        }
    }


    /// <summary>
    /// Génère une clé unique de paiement en concaténant l'ID et le nom de l'offre.
    /// </summary>
    /// <param name="offre">L'offre pour laquelle générer la clé de paiement.</param>
    /// <returns>La clé unique de paiement générée.</returns>
    private string GenereClePaiement(Offre offre)
    {
        // Concaténe l'ID de l'offre et le nom de l'offre pour former la clé unique
        string clePaiement = $"{offre.ID}_{offre.TypeOffre}";

       return clePaiement;
    }
}