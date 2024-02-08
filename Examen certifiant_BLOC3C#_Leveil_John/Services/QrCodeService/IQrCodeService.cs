namespace Examen_certifiant_BLOC3C__Leveil_John.Services.QrCodeService
{
    public interface IQrCodeService
    {
        byte[] GenerateQrCode(string cleCompte, string clePaiement);
    }
}
