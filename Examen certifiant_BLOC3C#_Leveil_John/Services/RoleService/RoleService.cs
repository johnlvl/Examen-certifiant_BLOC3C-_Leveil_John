using Microsoft.AspNetCore.Identity;

namespace Examen_certifiant_BLOC3C__Leveil_John.Services.RoleService
{
    public class RoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task InitializeRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Administrateur"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Administrateur"));
            }
            if (!await _roleManager.RoleExistsAsync("Utilisateur"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Utilisateur"));
            }
        }
    }
}
