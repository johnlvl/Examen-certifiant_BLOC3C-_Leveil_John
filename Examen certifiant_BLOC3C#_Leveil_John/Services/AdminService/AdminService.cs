using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Examen_certifiant_BLOC3C__Leveil_John.Services.AdminService;

public class AdminService : AuthorizationHandler<OperationAuthorizationRequirement>
{
    private readonly string _adminUserId;

    public AdminService(string adminUserId)
    {
        _adminUserId = adminUserId;
    }

    /// <summary>
    /// Gère la vérification des autorisations pour l'opération spécifiée.
    /// </summary>
    /// <param name="context">Le contexte d'autorisation.</param>
    /// <param name="requirement">Le requirement d'autorisation.</param>
    /// <returns>
    /// - Si l'utilisateur n'est pas authentifié, la vérification de l'autorisation échoue.
    /// - Si l'ID de l'utilisateur n'est pas trouvé dans les revendications, la vérification de l'autorisation échoue.
    /// - Si l'ID de l'utilisateur correspond à l'ID de l'administrateur, l'accès est autorisé.
    /// - Sinon, la vérification de l'autorisation échoue.
    /// </returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement)
    {
        // Vérifie si l'utilisateur est authentifié
        if (!context.User.Identity.IsAuthenticated)
        {
            return Task.CompletedTask; // L'utilisateur n'est pas authentifié, la vérification de l'autorisation échoue
        }

        // Récupère l'ID de l'utilisateur à partir des revendications (claims)
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Task.CompletedTask; // L'ID de l'utilisateur n'est pas trouvé dans les revendications, la vérification de l'autorisation échoue
        }

        var userId = userIdClaim.Value;

        // Vérifie si l'ID de l'utilisateur correspond à l'ID de l'administrateur
        if (userId == _adminUserId)
        {
            context.Succeed(requirement); // L'utilisateur est l'administrateur, autorise l'accès
        }

        return Task.CompletedTask; // L'ID de l'utilisateur ne correspond pas à l'ID de l'administrateur, la vérification de l'autorisation échoue
    }
}
