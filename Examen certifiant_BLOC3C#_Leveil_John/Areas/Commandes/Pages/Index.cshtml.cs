using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Examen_certifiant_BLOC3C__Leveil_John.Areas.Commandes.Pages;

public class IndexModel : PageModel
{
    private readonly Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext _context;

    public IndexModel(Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext context)
    {
        _context = context;
    }
    public void OnGet()
    {
    }
}
