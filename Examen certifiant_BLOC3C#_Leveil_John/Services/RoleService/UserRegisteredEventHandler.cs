using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Microsoft.AspNetCore.Identity;

namespace Examen_certifiant_BLOC3C__Leveil_John.Services.RoleService
{
    public class UserRegisteredEventHandler
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRegisteredEventHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Utilisateur");
            }
        }
    }
}
