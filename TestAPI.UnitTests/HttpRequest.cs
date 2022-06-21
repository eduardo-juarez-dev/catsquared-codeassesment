using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using TestAPI.Models;

namespace TestAPI.UnitTests;

// NOTE: These tests should be run when API is running.
public class HttpRequest
{
    private readonly HttpClient httpClient = new HttpClient();

    // Test if service returns 401 - unauthorized error when user is not authorized
    [Fact]
    public async void GetWeatherForecast_ShouldReturnUnauthorized_WhenTokenIsNotValid()
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
        
        HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5100/WeatherForecast?days=5");
        var status = response.StatusCode.ToString();
        Assert.Equal("Unauthorized", status);
    }
    
    // Test if service returns 200 - success, and result is a JSON object
    [Fact]
    public async void GetWeatherForecastUnauthenticated_ShouldReturnJSON_WithNoToken()
    {
        HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5100/WeatherForecast/unauthenticated?days=5");
        var status = response.StatusCode.ToString();
        var result = response.Content.Headers.ContentType?.MediaType;
        Assert.Equal("OK", status);
        Assert.Equal("application/json", result);
    }
    
    // Test if service returns a valid List<WeatherForecast>
    [Fact]
    public async void GetWeatherForecastUnauthenticated_ShouldReturnValidList_WithNoToken()
    {
        HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5100/WeatherForecast/unauthenticated?days=5");
        if (response.IsSuccessStatusCode)
        {
            var content= response.Content.ReadAsStringAsync().Result;
            var results = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);
            Assert.Equal(typeof(List<WeatherForecast>), results.GetType()); 
        }
        Assert.True(response.IsSuccessStatusCode);
    }    
}