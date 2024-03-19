using Examen_certifiant_BLOC3C__Leveil_John.Areas.Paniers.Pages;
using Examen_certifiant_BLOC3C__Leveil_John.Services.PaimentService;
using Examen_certifiant_BLOC3C__Leveil_John.Services.PanierService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using Xunit;
using Assert = Xunit.Assert;

namespace Test.Areas.Paniers;

public class PaniersTest
{
    [Fact]
    public void OnPostProcessPaiement_PaiementReussi_RedirectsToCommandes()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        // Crée un contexte de base de données en mémoire
        using (var context = new Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext(options))
        {
            // Ajoutez des données de test au contexte si nécessaire

            var paiementServiceMock = new Mock<Examen_certifiant_BLOC3C__Leveil_John.Services.PaimentService.PaimentService>();
            paiementServiceMock.Setup(m => m.ProcessPaiement(It.IsAny<decimal>())).Returns(true);

            var indexModel = new IndexModel(context, paiementServiceMock.Object);

            // Act
            var result = indexModel.OnPostProcessPaiement();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/Commandes", redirectToActionResult.Url);
        }
    }

    [Fact]
    public async Task OnGetAsync_WhenUserNotAuthenticated_RedirectsToLoginPage()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext(options))
        {
            var paiementServiceMock = new Mock<Examen_certifiant_BLOC3C__Leveil_John.Services.PaimentService.PaimentService>();
            paiementServiceMock.Setup(m => m.ProcessPaiement(It.IsAny<decimal>())).Returns(true);

            var indexModel = new IndexModel(context, paiementServiceMock.Object);

            // Crée un contexte de requête avec un utilisateur non authentifié
            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity())
            };

            // Assigne le contexte de requête à la PageModel
            indexModel.PageContext = new PageContext
            {
                HttpContext = httpContext
            };

            // Act
            await indexModel.OnGetAsync();

            // Assert
            Assert.Equal(302, httpContext.Response.StatusCode);
            Assert.StartsWith("/Identity/Account/Login", httpContext.Response.Headers["Location"]);
        }
    }

    [Fact]
    public void OnPostProcessPaiement_PaiementEchoue_RedirectsToAutrePage()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext(options))
        {
            var paiementServiceMock = new Mock<Examen_certifiant_BLOC3C__Leveil_John.Services.PaimentService.PaimentService>();
            paiementServiceMock.Setup(m => m.ProcessPaiement(It.IsAny<decimal>())).Returns(false);

            var indexModel = new IndexModel(context, paiementServiceMock.Object);

            // Act
            var result = indexModel.OnPostProcessPaiement();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/AutrePage", redirectToActionResult.Url);
        }
    }
}
