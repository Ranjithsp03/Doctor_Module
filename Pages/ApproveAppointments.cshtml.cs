using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

public class ApproveAppointmentsModel : PageModel
{
    private readonly HttpClient _httpClient;

    public ApproveAppointmentsModel(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public List<AppointmentViewModel> Appointments { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (TempData["DoctorID"] == null)
        {
            TempData["Error"] = "Doctor ID missing. Please login again.";
            return RedirectToPage("/Login");
        }

        string doctorId = TempData["DoctorID"].ToString();
        TempData.Keep("DoctorID");

        var response = await _httpClient.GetAsync($"http://localhost:5298/api/Appointment/doctor/{doctorId}");
        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            Appointments = JsonSerializer.Deserialize<List<AppointmentViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return Page();
    }

    public async Task<IActionResult> OnPostApproveAsync(Guid id)
    {
        var response = await _httpClient.PutAsync($"http://localhost:5298/api/Appointment/{id}/approve", null);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeclineAsync(Guid id)
    {
        var remarks = new StringContent("\"Declined by doctor\"", System.Text.Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"http://localhost:5298/api/Appointment/{id}/decline", remarks);
        return RedirectToPage();
    }

    public class AppointmentViewModel
    {
        public Guid AppointmentId { get; set; }
        public string PatientId { get; set; }
        public string Reason { get; set; }
        public DateTime TimeSlot { get; set; }
    }
}
