using Azure;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Examen_certifiant_BLOC3C__Leveil_John.Services.PaimentService;

public class PaimentService : IPaiementService
{
    /// <summary>
    /// Simule le traitement d'un paiement avec le montant spécifié.
    /// </summary>
    /// <param name="prix">Le montant du paiement à traiter.</param>
    /// <returns>Retourne `true` pour indiquer que le paiement a été réussi (simulation).</returns>
    public virtual bool ProcessPaiement(decimal prix)
    {
        // Simulation du paiement (toujours réussi dans ce cas fictif)
        return true;
    }
}
