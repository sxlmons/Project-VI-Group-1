using System.Net;
using System.Net.Http.Json;
using MarketPlaceBackend.DTOs;
using MarketPlaceBackend.Tests.Helpers;

namespace MarketPlaceBackend.Tests.Integration;

[TestFixture]
public class AuthIntegrationTests
{
    private TestWebApplicationFactory _factory;
    private HttpClient _client;

    [SetUp]
    public void Setup()
    {
        _factory = new TestWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _client.Dispose();
        _factory.Dispose();
    }

    [Test]
    public async Task Register_ThenLogin_Succeeds()
    {
        // Register 
        await _client.PostAsJsonAsync("/api/auth/register",
            new RegisterRequest
            {
                Email = "test@test.com",
                Password = "ValidPass1!"
            });

        // Login with those same credentials
        var response = await _client.PostAsJsonAsync("/api/auth/login",
            new LoginRequest
            {
                Email = "test@test.com",
                Password = "ValidPass1!"
            });

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
