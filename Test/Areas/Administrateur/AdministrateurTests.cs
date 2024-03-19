using Examen_certifiant_BLOC3C__Leveil_John.Areas.Administrateur.Pages;
using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Examen_certifiant_BLOC3C__Leveil_John.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Test.Areas.Administrateur;

public class AdministrateurTests
{
    [Fact]
    public void GetTotalCommandeParOffre_ReturnsCorrectDictionary()
    {
        // Crée des réservations simulées avec différents types d'offres
        var reservationsSimulees = new List<Reservation>
            {
                new Reservation { Offre = new Offre { TypeOffre = "Type1" } },
                new Reservation { Offre = new Offre { TypeOffre = "Type2" } },
                new Reservation { Offre = new Offre { TypeOffre = "Type1" } },
                new Reservation { Offre = new Offre { TypeOffre = "Type3" } },
                new Reservation { Offre = new Offre { TypeOffre = "Type2" } },
            };

        // Configure un DbSet simulé contenant les réservations simulées
        var mockDbSet = new Mock<DbSet<Reservation>>();
        mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservationsSimulees.AsQueryable().Provider);
        mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservationsSimulees.AsQueryable().Expression);
        mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservationsSimulees.AsQueryable().ElementType);
        mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(reservationsSimulees.AsQueryable().GetEnumerator());

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        // Configure le contexte de données simulé pour renvoyer le DbSet simulé
        var mockContext = new Mock<ApplicationDbContext>(options);
        mockContext.Setup(c => c.Reservations).Returns(mockDbSet.Object);

        // Crée une instance de la classe à tester en utilisant le contexte de données simulé
        var instance = new IndexModel(mockContext.Object, null, null);

        // Exécute la méthode à tester
        var result = instance.GetTotalCommandeParOffre();

        // Vérifie si le dictionnaire retourné contient les bonnes valeurs
        Assert.Equal(2, result["Type1"]); // Il devrait y avoir 2 réservations de Type1
        Assert.Equal(2, result["Type2"]); // Il devrait y avoir 2 réservations de Type2
        Assert.Equal(1, result["Type3"]); // Il devrait y avoir 1 réservation de Type3
    }

    [Fact]
    public void GetTotalCommande_ReturnsCorrectCount()
    {
        var reservationsSimulees = new List<Reservation>
    {
        new Reservation(),
        new Reservation(),
        new Reservation()
    };

        var mockDbSet = new Mock<DbSet<Reservation>>();
        mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservationsSimulees.AsQueryable().Provider);
        mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservationsSimulees.AsQueryable().Expression);
        mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservationsSimulees.AsQueryable().ElementType);
        mockDbSet.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(reservationsSimulees.AsQueryable().GetEnumerator());

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var mockContext = new Mock<ApplicationDbContext>(options);
        mockContext.Setup(c => c.Reservations).Returns(mockDbSet.Object);

        var instance = new IndexModel(mockContext.Object, null, null);

        var result = instance.GetTotalCommande();

        Assert.Equal(3, result); // Il devrait y avoir 3 réservations simulées
    }

    [Fact]
    public void GetTotalUtilisateur_ReturnsCorrectCount()
    {
        var utilisateursSimules = new List<ApplicationUser>
    {
        new ApplicationUser(),
        new ApplicationUser(),
        new ApplicationUser()
    };

        var mockDbSet = new Mock<DbSet<ApplicationUser>>();
        mockDbSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Provider).Returns(utilisateursSimules.AsQueryable().Provider);
        mockDbSet.As<IQueryable<ApplicationUser>>().Setup(m => m.Expression).Returns(utilisateursSimules.AsQueryable().Expression);
        mockDbSet.As<IQueryable<ApplicationUser>>().Setup(m => m.ElementType).Returns(utilisateursSimules.AsQueryable().ElementType);
        mockDbSet.As<IQueryable<ApplicationUser>>().Setup(m => m.GetEnumerator()).Returns(utilisateursSimules.AsQueryable().GetEnumerator());

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var mockContext = new Mock<ApplicationDbContext>(options);
        mockContext.Setup(c => c.Users).Returns(mockDbSet.Object);

        var instance = new IndexModel(mockContext.Object, null, null);

        var result = instance.GetTotalUtilisateur();

        Assert.Equal(3, result); // Il devrait y avoir 3 utilisateurs simulés
    }
}
