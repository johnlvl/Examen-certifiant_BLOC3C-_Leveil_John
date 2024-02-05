using System.Text.Json;

namespace Examen_certifiant_BLOC3C__Leveil_John.Services.PanierService;

public static class PanierService
{
    private const string PanierKey = "Panier";

    /// <summary>
    /// Récupère un objet désérialisé depuis la session en utilisant la clé spécifiée.
    /// </summary>
    /// <typeparam name="T">Le type de l'objet à récupérer.</typeparam>
    /// <param name="session">La session en cours.</param>
    /// <param name="key">La clé associée à l'objet dans la session.</param>
    /// <returns>L'objet désérialisé ou la valeur par défaut de son type si absent.</returns>
    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var data = session.GetString(key);
        return data == null ? default(T) : JsonSerializer.Deserialize<T>(data);
    }

    /// <summary>
    /// Stocke un objet sérialisé en format JSON dans la session avec la clé spécifiée.
    /// </summary>
    /// <param name="session">La session en cours.</param>
    /// <param name="key">La clé associée à l'objet dans la session.</param>
    /// <param name="value">L'objet à sérialiser et stocker.</param>
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    /// <summary>
    /// Récupère le dictionnaire représentant le panier depuis la session.
    /// </summary>
    /// <param name="session">La session en cours.</param>
    /// <returns>Le dictionnaire représentant le panier, ou un nouveau dictionnaire vide si absent.</returns>
    public static Dictionary<int, int> GetPanierArticle(this ISession session)
    {
        // Récupère le dictionnaire du panier depuis la session
        var panier = session.GetObjectFromJson<Dictionary<int, int>>(PanierKey);

        // Si le panier est null, initialise un nouveau dictionnaire
        return panier ?? new Dictionary<int, int>();
    }

    /// <summary>
    /// Stocke un dictionnaire représentant le panier dans la session.
    /// </summary>
    /// <param name="session">La session en cours.</param>
    /// <param name="panier">Le dictionnaire représentant le panier à stocker.</param>
    public static void SetArticlePanier(this ISession session, Dictionary<int, int> panier)
    {
        // Stocke le dictionnaire du panier dans la session
        session.SetObjectAsJson(PanierKey, panier);
    }


    /// <summary>
    /// Ajoute un article identifié par son ID au panier en session.
    /// </summary>
    /// <param name="session">La session en cours.</param>
    /// <param name="id">L'ID de l'article à ajouter au panier.</param>
    public static void AjouterAuPanier(this ISession session, int id)
    {
        // Récupère le dictionnaire du panier depuis la session
        var panier = session.GetPanierArticle();

        // Vérifie si l'offre est déjà dans le panier
        if (panier.ContainsKey(id))
        {
            // L'offre est déjà dans le panier, incrémente la quantité
            panier[id]++;
        }
        else
        {
            // L'offre n'est pas dans le panier, ajoute l'offre avec une quantité de 1
            panier.Add(id, 1);
        }

        // Stocke le dictionnaire du panier mis à jour dans la session
        session.SetArticlePanier(panier);
    }
}
