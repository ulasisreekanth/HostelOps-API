using Microsoft.AspNetCore.Mvc;
using HostelOps_API.Controllers;

namespace HostelOps_API_Unit_Tests.Controllers;

/// <summary>
/// Unit tests for <see cref="WeatherForecastController"/>.
/// These tests validate the controller logic in isolation without starting the HTTP server.
/// </summary>
public class WeatherForecastControllerTests
{
    private readonly WeatherForecastController _controller;

    public WeatherForecastControllerTests()
    {
        _controller = new WeatherForecastController();
    }

    // ──────────────────────────────────────────────
    //  GET /WeatherForecast — Return value shape
    // ──────────────────────────────────────────────

    [Fact(DisplayName = "Get returns exactly 5 forecasts")]
    public void Get_ReturnsExactlyFiveForecasts()
    {
        // Act
        var result = _controller.Get();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5, result.Count());
    }

    [Fact(DisplayName = "Get never returns null collection")]
    public void Get_DoesNotReturnNull()
    {
        var result = _controller.Get();
        Assert.NotNull(result);
    }

    [Fact(DisplayName = "Every forecast has a non-empty Summary")]
    public void Get_EachForecastHasNonEmptySummary()
    {
        var result = _controller.Get();

        foreach (var forecast in result)
        {
            Assert.False(
                string.IsNullOrWhiteSpace(forecast.Summary),
                $"Forecast dated {forecast.Date} has a null or empty Summary.");
        }
    }

    [Fact(DisplayName = "Every forecast Summary is one of the known values")]
    public void Get_SummaryIsFromKnownSet()
    {
        string[] knownSummaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        var result = _controller.Get();

        foreach (var forecast in result)
        {
            Assert.Contains(forecast.Summary, knownSummaries);
        }
    }

    // ──────────────────────────────────────────────
    //  Date range assertions
    // ──────────────────────────────────────────────

    [Fact(DisplayName = "Forecast dates start from tomorrow")]
    public void Get_FirstDateIsTomorrow()
    {
        var result = _controller.Get().ToList();
        var tomorrow = DateOnly.FromDateTime(DateTime.Now.AddDays(1));

        Assert.Equal(tomorrow, result[0].Date);
    }

    [Fact(DisplayName = "Forecast dates are consecutive daily")]
    public void Get_DatesAreConsecutive()
    {
        var dates = _controller.Get().Select(f => f.Date).ToList();

        for (int i = 1; i < dates.Count; i++)
        {
            Assert.Equal(dates[i - 1].AddDays(1), dates[i]);
        }
    }

    [Fact(DisplayName = "Last forecast date is 5 days from today")]
    public void Get_LastDateIsFiveDaysFromNow()
    {
        var result = _controller.Get().ToList();
        var expectedLastDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5));

        Assert.Equal(expectedLastDate, result[^1].Date);
    }

    // ──────────────────────────────────────────────
    //  Temperature range assertions
    // ──────────────────────────────────────────────

    [Fact(DisplayName = "TemperatureC is within the expected range [-20, 55)")]
    public void Get_TemperatureCIsWithinRange()
    {
        // Run multiple times because values are random
        for (int run = 0; run < 10; run++)
        {
            var result = _controller.Get();
            foreach (var forecast in result)
            {
                Assert.InRange(forecast.TemperatureC, -20, 54);
            }
        }
    }

    [Fact(DisplayName = "TemperatureF is correctly derived from TemperatureC")]
    public void Get_TemperatureFIsCorrectlyCalculated()
    {
        var result = _controller.Get();

        foreach (var forecast in result)
        {
            int expectedF = 32 + (int)(forecast.TemperatureC / 0.5556);
            Assert.Equal(expectedF, forecast.TemperatureF);
        }
    }

    // ──────────────────────────────────────────────
    //  Repeated calls
    // ──────────────────────────────────────────────

    [Fact(DisplayName = "Successive calls each return 5 items")]
    public void Get_MultipleCallsEachReturnFiveItems()
    {
        for (int i = 0; i < 5; i++)
        {
            var result = _controller.Get();
            Assert.Equal(5, result.Count());
        }
    }
}
