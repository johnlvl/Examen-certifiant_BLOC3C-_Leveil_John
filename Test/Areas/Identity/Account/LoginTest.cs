using Examen_certifiant_BLOC3C__Leveil_John.Areas.Identity.Pages.Account;
using Examen_certifiant_BLOC3C__Leveil_John.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Assert = Xunit.Assert;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Test.Areas.Identity.Account;

public class LoginTests
{
    [Fact]
    public async Task PasswordSignInAsync_UserNotFound_ReturnsFailed()
    {
        // Arrange
        var userName = "utilisateurInconnu";
        var password = "motDePasse";
        var isPersistent = false;
        var lockoutOnFailure = false;

        var userManagerMock = new Mock<UserManager<ApplicationUser>>(
            new Mock<IUserStore<ApplicationUser>>().Object,
            null, null, null, null, null, null, null, null);

        var contextAccessorMock = new Mock<IHttpContextAccessor>();
        var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

        var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            userManagerMock.Object,
            contextAccessorMock.Object,
            claimsFactoryMock.Object,
            null, null, null, null);

        signInManagerMock.Setup(m => m.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure))
                         .ReturnsAsync(SignInResult.Failed);

        var loginModel = new LoginModel(signInManagerMock.Object, Mock.Of<ILogger<LoginModel>>());

        // Act
        var result = await signInManagerMock.Object.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

        // Assert
        Assert.Equal(SignInResult.Failed, result);
    }

    [Fact]
    public async Task PasswordSignInAsync_UserFound_ReturnsSuccess()
    {
        // Arrange
        var userName = "utilisateurTrouve";
        var password = "motDePasse";
        var isPersistent = false;
        var lockoutOnFailure = false;

        var userManagerMock = new Mock<UserManager<ApplicationUser>>(
            new Mock<IUserStore<ApplicationUser>>().Object,
            null, null, null, null, null, null, null, null);

        // Configuration du mock pour retourner un utilisateur lors de l'appel à FindByNameAsync
        userManagerMock.Setup(m => m.FindByNameAsync(userName))
                       .ReturnsAsync(new ApplicationUser { UserName = userName });

        var contextAccessorMock = new Mock<IHttpContextAccessor>();
        var claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

        var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            userManagerMock.Object,
            contextAccessorMock.Object,
            claimsFactoryMock.Object,
            null, null, null, null);

        // Configuration du mock pour retourner Success lors de l'appel à PasswordSignInAsync
        signInManagerMock.Setup(m => m.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure))
                         .ReturnsAsync(SignInResult.Success);

        var loginModel = new LoginModel(signInManagerMock.Object, Mock.Of<ILogger<LoginModel>>());

        // Act
        var result = await signInManagerMock.Object.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

        // Assert
        Assert.Equal(SignInResult.Success, result);
    }
}