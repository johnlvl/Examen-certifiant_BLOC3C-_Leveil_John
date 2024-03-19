using Examen_certifiant_BLOC3C__Leveil_John.Areas.Offres.Pages;
using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Test.Areas.Offres;

public class OffresTests
{
    [Fact]
    public void OnPostAjouterAuPanier_ReturnsNotFound_When_OffreDoesNotExist()
    {
        // Arrange
        var mockOffres = new List<Offre>(); // Simule une liste vide d'offres
        var mockDbSet = new Mock<DbSet<Offre>>();
        mockDbSet.As<IQueryable<Offre>>().Setup(m => m.Provider).Returns(mockOffres.AsQueryable().Provider);
        mockDbSet.As<IQueryable<Offre>>().Setup(m => m.Expression).Returns(mockOffres.AsQueryable().Expression);
        mockDbSet.As<IQueryable<Offre>>().Setup(m => m.ElementType).Returns(mockOffres.AsQueryable().ElementType);
        mockDbSet.As<IQueryable<Offre>>().Setup(m => m.GetEnumerator()).Returns(mockOffres.AsQueryable().GetEnumerator());

        var contextOptions = new DbContextOptionsBuilder<Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        var mockContext = new Mock<Examen_certifiant_BLOC3C__Leveil_John.Data.ApplicationDbContext>(contextOptions);
        mockContext.Setup(c => c.Offres).Returns(mockDbSet.Object);

        var httpContextMock = new Mock<HttpContext>();
        var sessionMock = new Mock<ISession>();

        var pageModel = new IndexModel(mockContext.Object)
        {
            PageContext = new PageContext { HttpContext = httpContextMock.Object }
        };

        httpContextMock.Setup(c => c.Session).Returns(sessionMock.Object);

        // Act
        var result = pageModel.OnPostAjouterAuPanier(999); // ID inexistant

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
    }

    [Fact]
    public void OnPostAjouterAuPanier_AddsOffreToSessionAndRedirects_When_OffreExists()
    {
        // Arrange
        var offresSimulees = new List<Offre>
        {
        new Offre { ID = 1 }
        };

        var mockDbSet = new Mock<DbSet<Offre>>();
        mockDbSet.As<IQueryable<Offre>>().Setup(m => m.Provider).Returns(offresSimulees.AsQueryable().Provider);
        mockDbSet.As<IQueryable<Offre>>().Setup(m => m.Expression).Returns(offresSimulees.AsQueryable().Expression);
        mockDbSet.As<IQueryable<Offre>>().Setup(m => m.ElementType).Returns(offresSimulees.AsQueryable().ElementType);
        mockDbSet.As<IQueryable<Offre>>().Setup(m => m.GetEnumerator()).Returns(offresSimulees.AsQueryable().GetEnumerator());

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        var mockContext = new Mock<ApplicationDbContext>(options);
        mockContext.Setup(c => c.Offres).Returns(mockDbSet.Object);

        var pageModel = new IndexModel(mockContext.Object)
        {
            PageContext = new PageContext { HttpContext = new DefaultHttpContext() }
        };

        var httpContextMock = new Mock<HttpContext>();
        var sessionMock = new Mock<ISession>();
        httpContextMock.Setup(c => c.Session).Returns(sessionMock.Object);
        pageModel.PageContext.HttpContext = httpContextMock.Object;

        // Act
        var result = pageModel.OnPostAjouterAuPanier(1); // ID existant

        // Assert
        var redirectResult = Assert.IsType<RedirectResult>(result);
        Assert.Equal("/Paniers", redirectResult.Url);
    }
}
