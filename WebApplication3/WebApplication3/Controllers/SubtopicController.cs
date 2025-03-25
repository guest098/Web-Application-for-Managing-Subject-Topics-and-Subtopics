using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("subtopics")]
    public class SubtopicController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "https://localhost:7148/api/subtopics";

        public SubtopicController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet("GetAllSubtopics")]
        public async Task<IActionResult> GetAllSubtopics()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var subtopics = JsonSerializer.Deserialize<List<SubtopicViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Json(subtopics);
        }

        [HttpPost("AddSubtopic")]
        public async Task<IActionResult> AddSubtopic([FromBody] SubtopicViewModel subtopic)
        {
            var json = JsonSerializer.Serialize(subtopic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiBaseUrl, content);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            return Ok();
        }

        [HttpPut("EditSubtopic/{id}")]
        public async Task<IActionResult> EditSubtopic(int id, [FromBody] SubtopicViewModel subtopic)
        {
            if (id != subtopic.Id)
                return BadRequest("Subtopic ID mismatch");

            var json = JsonSerializer.Serialize(subtopic);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}/{id}", content);
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            return Ok();
        }

        [HttpDelete("DeleteSubtopic/{id}")]
        public async Task<IActionResult> DeleteSubtopic(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            return Ok();
        }
    }
}
