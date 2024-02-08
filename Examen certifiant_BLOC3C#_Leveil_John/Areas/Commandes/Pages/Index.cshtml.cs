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

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            // R�cup�re l'utilisateur actuellement connect�
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.GetUserAsync(User);

            // Cr�e une nouvelle entr�e dans la table Panier
            var nouveauPanier = new Panier
            {
                UtilisateurId = userId
            };

            // Ajoute les offres du panier � la collection Offres du nouveau panier
            var panier = HttpContext.Session.GetPanierArticle();
            foreach (var offreId in panier.Keys)
            {
                var offre = await _context.Offres.FindAsync(offreId);
                nouveauPanier.Offres.Add(offre);

                // Ajoute l'offre � la liste PanierArticle
                PanierArticle.Add(offre);
            }

            // Enregistre le nouveau panier avec les offres
            _context.Paniers.Add(nouveauPanier);
            await _context.SaveChangesAsync();

            // Supprime le panier de la session apr�s avoir pass� la commande
            HttpContext.Session.Remove("Panier");

            // Cr�e une liste pour stocker les images des QR codes
            List<byte[]> qrCodeImages = new List<byte[]>();

            // Cr�e les r�servations pour chaque offre dans le panier
            foreach (var offreId in panier.Keys)
            {
                var offre = await _context.Offres.FindAsync(offreId);

                // G�n�re la cl� unique pour cette offre
                string clePaiement = GenereClePaiement(offre);

                Reservation reservation = new Reservation();
                reservation.ClefPaiment = clePaiement;
                reservation.OffreId = offreId;
                reservation.ClientId = userId;
                reservation.StatutPaiement = "OK";

                _context.Reservations.Add(reservation);

                if (user != null && !string.IsNullOrEmpty(user.CleCompte))
                {
                    var cleCompte = user.CleCompte;

                    // G�n�re le QR code et le stocke dans la liste
                    byte[] qrCodeImage = _qrCodeService.GenerateQrCode(cleCompte, clePaiement);
                    qrCodeImages.Add(qrCodeImage);
                }

            }
            // Affecte la liste des images des QR codes � la propri�t� correspondante
            QrCodeImages = qrCodeImages;

            await _context.SaveChangesAsync();

            return Page();
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Erreur lors du paiement. Veuillez r�ssayer.");
        }
    }


    private string GenereClePaiement(Offre offre)
    {
        // Concat�ne l'ID de l'offre et le nom de l'offre pour former la cl� unique
        string clePaiement = $"{offre.ID}_{offre.TypeOffre}";

       return clePaiement;
    }
}