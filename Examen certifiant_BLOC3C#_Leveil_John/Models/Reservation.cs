using Examen_certifiant_BLOC3C__Leveil_John.Data;

namespace Examen_certifiant_BLOC3C__Leveil_John.Models;

public class Reservation
{
    public int ID { get; set; }

    public string StatutPaiement { get; set; }

    public string ClefPaiment { get; set; }

    public string ClientId { get; set; }

    public int OffreId { get; set; }

    public virtual Offre Offre { get; set; }

    public byte[] QrCodeImageData { get; set; }

}
