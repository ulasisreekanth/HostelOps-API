using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HostelOps_API_Integration_Tests.Controllers;

/// <summary>
/// Integration tests for GET /WeatherForecast.
/// Spins up the real ASP.NET Core pipeline via <see cref="WebApplicationFactory{TEntryPoint}"/>
/// so every middleware, routing, and serialisation layer is exercised.
/// </summary>
public class WeatherForecastIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    // Shared JSON options matching ASP.NET Core's default serialiser settings
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public WeatherForecastIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    // ──────────────────────────────────────────────
    //  HTTP status & Content-Type
    // ──────────────────────────────────────────────

    [Fact(DisplayName = "GET /WeatherForecast returns 200 OK")]
    public async Task Get_ReturnsHttpOk()
    {
        var response = await _client.GetAsync("/WeatherForecast");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact(DisplayName = "GET /WeatherForecast returns application/json")]
    public async Task Get_ReturnsJsonContentType()
    {
        var response = await _client.GetAsync("/WeatherForecast");
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.StartsWith("application/json", response.Content.Headers.ContentType!.MediaType);
    }

    // ──────────────────────────────────────────────
    //  Payload shape
    // ──────────────────────────────────────────────

    [Fact(DisplayName = "GET /WeatherForecast body is a JSON array")]
    public async Task Get_BodyIsJsonArray()
    {
        var body = await _client.GetStringAsync("/WeatherForecast");
        using var doc = JsonDocument.Parse(body);
        Assert.Equal(JsonValueKind.Array, doc.RootElement.ValueKind);
    }

    [Fact(DisplayName = "GET /WeatherForecast returns exactly 5 items")]
    public async Task Get_ReturnsExactlyFiveItems()
    {
        var body = await _client.GetStringAsync("/WeatherForecast");
        using var doc = JsonDocument.Parse(body);
        Assert.Equal(5, doc.RootElement.GetArrayLength());
    }

    [Fact(DisplayName = "Each forecast item has the required properties")]
    public async Task Get_EachItemHasRequiredProperties()
    {
        var body = await _client.GetStringAsync("/WeatherForecast");
        using var doc = JsonDocument.Parse(body);

        foreach (var element in doc.RootElement.EnumerateArray())
        {
            Assert.True(element.TryGetProperty("date", out _), "Missing 'date' property");
            Assert.True(element.TryGetProperty("temperatureC", out _), "Missing 'temperatureC' property");
            Assert.True(element.TryGetProperty("temperatureF", out _), "Missing 'temperatureF' property");
            Assert.True(element.TryGetProperty("summary", out _), "Missing 'summary' property");
        }
    }

    // ──────────────────────────────────────────────
    //  Field value assertions
    // ──────────────────────────────────────────────

    [Fact(DisplayName = "Each forecast summary is non-empty")]
    public async Task Get_EachSummaryIsNonEmpty()
    {
        var body = await _client.GetStringAsync("/WeatherForecast");
        using var doc = JsonDocument.Parse(body);

        foreach (var element in doc.RootElement.EnumerateArray())
        {
            var summary = element.GetProperty("summary").GetString();
            Assert.False(string.IsNullOrWhiteSpace(summary), "Summary should not be null or empty");
        }
    }

    [Fact(DisplayName = "Each forecast temperatureC is within [-20, 55) range")]
    public async Task Get_TemperatureCIsWithinRange()
    {
        var body = await _client.GetStringAsync("/WeatherForecast");
        using var doc = JsonDocument.Parse(body);

        foreach (var element in doc.RootElement.EnumerateArray())
        {
            int tempC = element.GetProperty("temperatureC").GetInt32();
            Assert.InRange(tempC, -20, 54);
        }
    }

    [Fact(DisplayName = "Each forecast date is in the future")]
    public async Task Get_AllDatesAreInTheFuture()
    {
        var body = await _client.GetStringAsync("/WeatherForecast");
        using var doc = JsonDocument.Parse(body);
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        foreach (var element in doc.RootElement.EnumerateArray())
        {
            var rawDate = element.GetProperty("date").GetString()!;
            var date = DateOnly.Parse(rawDate);
            Assert.True(date > today, $"Date {date} should be after today ({today})");
        }
    }

    [Fact(DisplayName = "temperatureF is the F-equivalent of temperatureC")]
    public async Task Get_TemperatureFMatchesExpectedConversion()
    {
        var body = await _client.GetStringAsync("/WeatherForecast");
        using var doc = JsonDocument.Parse(body);

        foreach (var element in doc.RootElement.EnumerateArray())
        {
            int tempC = element.GetProperty("temperatureC").GetInt32();
            int tempF = element.GetProperty("temperatureF").GetInt32();
            int expectedF = 32 + (int)(tempC / 0.5556);
            Assert.Equal(expectedF, tempF);
        }
    }

    // ──────────────────────────────────────────────
    //  Error / negative scenarios
    // ──────────────────────────────────────────────

    [Fact(DisplayName = "POST /WeatherForecast returns 405 Method Not Allowed")]
    public async Task Post_ReturnsMethodNotAllowed()
    {
        var response = await _client.PostAsync("/WeatherForecast", null);
        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }

    [Fact(DisplayName = "GET /WeatherForecast with unknown suffix returns 404")]
    public async Task Get_UnknownRoute_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/WeatherForecast/unknownroute");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // ──────────────────────────────────────────────
    //  Deserialization round-trip
    // ──────────────────────────────────────────────

    [Fact(DisplayName = "Response deserializes cleanly into WeatherForecast models")]
    public async Task Get_DeserializesIntoModels()
    {
        var forecasts = await _client.GetFromJsonAsync<List<WeatherForecastDto>>(
            "/WeatherForecast", JsonOptions);

        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts!.Count);
        Assert.All(forecasts, f =>
        {
            Assert.NotNull(f.Summary);
            Assert.InRange(f.TemperatureC, -20, 54);
        });
    }

    // ──────────────────────────────────────────────
    //  Concurrency
    // ──────────────────────────────────────────────

    [Fact(DisplayName = "Concurrent requests all return 200 OK")]
    public async Task Get_ConcurrentRequests_AllReturn200()
    {
        var tasks = Enumerable.Range(0, 10)
            .Select(_ => _client.GetAsync("/WeatherForecast"));

        var responses = await Task.WhenAll(tasks);

        Assert.All(responses, r => Assert.Equal(HttpStatusCode.OK, r.StatusCode));
    }
}

// ──────────────────────────────────────────────
//  Local DTO for deserialization round-trip test
// ──────────────────────────────────────────────

file record WeatherForecastDto(
    DateOnly Date,
    int TemperatureC,
    int TemperatureF,
    string? Summary);
