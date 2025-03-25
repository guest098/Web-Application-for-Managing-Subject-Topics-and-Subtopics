using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("subjects")]
    public class SubjectController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7148/api/subjects";

        public SubjectController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        [HttpGet("GetAllSubjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, new { error = "Failed to load subjects." });

            var json = await response.Content.ReadAsStringAsync();

            try
            {
                var subjects = JsonSerializer.Deserialize<List<SubjectViewModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles // ✅ Fix serialization issue
                });

                return Json(subjects);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing JSON: {ex.Message}");
                return StatusCode(500, new { error = "Invalid API response format." });
            }
        }


        [HttpPost("AddSubject")]
        public async Task<IActionResult> AddSubject([FromBody] SubjectViewModel subject)
        {
            var json = JsonSerializer.Serialize(subject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl, content);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            return Ok();
        }

        [HttpPut("/{id}")]
        public async Task<IActionResult> EditSubject(int id, [FromBody] SubjectViewModel subject)
        {
            if (id != subject.Id)
                return BadRequest("Subject ID mismatch");

            var json = JsonSerializer.Serialize(subject);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{id}", content);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            return Ok();
        }

        [HttpDelete("DeleteSubject/{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            return Ok();
        }
    }
}
