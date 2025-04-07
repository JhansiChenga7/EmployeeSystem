using EmployeeService.Models;
using EmployeeTenantResolver.Models;
using EmployeeUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;

    public HomeController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Step 1: Display the Tenant selection view (Index.cshtml)
    public IActionResult Index()
    {
        var tenants = new List<string> { "tenant1", "tenant2", "tenant3" };
        ViewBag.Tenants = tenants;
        return View();
    }

    // Step 2: Get tenant details after tenantId is selected and redirect to the login page
    [HttpPost]
    public async Task<IActionResult> GetTenantDetails(string tenantId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7276/api/EmployeeTenantResolver/{tenantId}");
        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var tenantInfo = JsonSerializer.Deserialize<TenantInfo>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            // Pass the tenantInfo to the login page
            return View("Login", tenantInfo);
        }

        return View("Error");
    }

    // Step 3: Handle login and fetch employee details
    [HttpPost]
    public async Task<IActionResult> GetEmployee(string tenantId, string username, string password)
    {
        // Step 3.1: Get Tenant URL and authenticate
        var request1 = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7276/api/EmployeeTenantResolver/{tenantId}");
        var response1 = await _httpClient.SendAsync(request1);
        if (response1.IsSuccessStatusCode)
        {
            var json1 = await response1.Content.ReadAsStringAsync();
            var tenantInfo = JsonSerializer.Deserialize<TenantInfo>(json1, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var tenantUrl = tenantInfo.tenantUrl;

            // Step 3.2: Get JWT Token
            var tokenResponse = await _httpClient.PostAsync($"{tenantUrl}/api/auth/login", new StringContent(
                JsonSerializer.Serialize(new { Username = username, Password = password }), Encoding.UTF8, "application/json"));

            if (tokenResponse.IsSuccessStatusCode)
            {
                var token = JsonSerializer.Deserialize<JsonElement>(await tokenResponse.Content.ReadAsStringAsync()).GetProperty("Token").GetString();

                // Step 3.3: Get Employee details with JWT Token
                var request2 = new HttpRequestMessage(HttpMethod.Get, $"{tenantUrl}/api/employee");
                request2.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response2 = await _httpClient.SendAsync(request2);

                if (response2.IsSuccessStatusCode)
                {
                    var json = await response2.Content.ReadAsStringAsync();
                    var employee = JsonSerializer.Deserialize<List<Employee>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    // Return employee details to EmployeeDetails view
                    return View("EmployeeDetails", new EmployeeDetailsViewModel
                    {
                        employee = employee,
                        tenantInfo = tenantInfo
                    });
                }
            }
        }

        return View("Error");
    }
}
