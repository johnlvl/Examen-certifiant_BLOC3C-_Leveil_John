using Examen_certifiant_BLOC3C__Leveil_John.Models;
using System.Text.Json;

namespace Examen_certifiant_BLOC3C__Leveil_John.Data
{
    public static class SessionExtensions
    {
        private const string PanierKey = "Panier";

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            return data == null ? default(T) : JsonSerializer.Deserialize<T>(data);
        }

        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static Dictionary<int, int> GetPanierArticle(this ISession session)
        {
            // Récupère le dictionnaire du panier depuis la session
            var panier = session.GetObjectFromJson<Dictionary<int, int>>(PanierKey);

            // Si le panier est null, initialise un nouveau dictionnaire
            return panier ?? new Dictionary<int, int>();
        }

        public static void SetArticlePanier(this ISession session, Dictionary<int, int> panier)
        {
            // Stocke le dictionnaire du panier dans la session
            session.SetObjectAsJson(PanierKey, panier);
        }

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
}
