using Microsoft.AspNetCore.Mvc.Testing;

namespace HostelOps_API_Integration_Tests;

public class ApplicationStartupTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApplicationStartupTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact(DisplayName = "The web application starts and answers HTTP requests")]
    public async Task Application_Starts_AndAnswersHttpRequests()
    {
        var response = await _client.GetAsync("/");

        Assert.True(
            (int)response.StatusCode < 500,
            $"Application returned {(int)response.StatusCode} {response.StatusCode}.");
    }
}
