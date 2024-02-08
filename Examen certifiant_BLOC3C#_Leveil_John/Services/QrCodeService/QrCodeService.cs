using QRCoder;
using System.Drawing;

namespace Examen_certifiant_BLOC3C__Leveil_John.Services.QrCodeService;

public class QrCodeService
{
    /// <summary>
    /// Génère un code QR à partir des clés de compte et de paiement spécifiées et le retourne sous forme de tableau de bytes.
    /// </summary>
    /// <param name="cleCompte">La clé de compte associée à l'utilisateur.</param>
    /// <param name="clePaiement">La clé de paiement unique générée.</param>
    /// <returns>Le Qr code généré sous forme de tableau de bytes.</returns>
    public byte[] GenerateQrCode(string cleCompte, string clePaiement)
    {
        // Concaténe les clés
        string combinedKeys = cleCompte + clePaiement;

        // Génére le QR code
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(combinedKeys, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);

        // Converti le QR code en image bitmap
        Bitmap qrCodeImage = qrCode.GetGraphic(20);

        // Converti l'image bitmap en tableau de bytes
        using (var stream = new System.IO.MemoryStream())
        {
            qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }
    }
}
