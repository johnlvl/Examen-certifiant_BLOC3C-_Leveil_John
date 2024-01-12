using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Newtonsoft.Json;

namespace Examen_certifiant_BLOC3C__Leveil_John.Data
{
    public static class SessionExtensions
    {
        private const string PanierKey = "Panier";

        public static void SetPanier(this ISession session, Panier panier)
        {
            session.SetString(PanierKey, JsonConvert.SerializeObject(panier));
        }

        public static Panier GetPanier(this ISession session)
        {
            var panierString = session.GetString(PanierKey);
            return panierString == null ? new Panier() : JsonConvert.DeserializeObject<Panier>(panierString);
        }
    }
}
