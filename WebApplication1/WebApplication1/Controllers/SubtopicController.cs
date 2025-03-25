using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/subtopics")]
    [ApiController]
    public class SubtopicController : ControllerBase
    {
        private readonly ISubtopicService _subtopicService;

        public SubtopicController(ISubtopicService subtopicService)
        {
            _subtopicService = subtopicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubtopics()
        {
            var subtopics = await _subtopicService.GetAllSubtopicsAsync();
            return Ok(subtopics);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubtopicById(int id)
        {
            var subtopic = await _subtopicService.GetSubtopicByIdAsync(id);
            if (subtopic == null) return NotFound();
            return Ok(subtopic);
        }

        [HttpGet("subject/{subjectId}")]
        public async Task<IActionResult> GetSubtopicsBySubjectId(int subjectId)
        {
            var subtopics = await _subtopicService.GetSubtopicsBySubjectIdAsync(subjectId);
            return Ok(subtopics);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubtopic([FromBody] Subtopic subtopic)
        {
            await _subtopicService.AddSubtopicAsync(subtopic);
            return CreatedAtAction(nameof(GetSubtopicById), new { id = subtopic.Id }, subtopic);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubtopic(int id, [FromBody] Subtopic subtopic)
        {
            if (id != subtopic.Id) return BadRequest("Subtopic ID mismatch");
            await _subtopicService.UpdateSubtopicAsync(subtopic);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubtopic(int id)
        {
            await _subtopicService.DeleteSubtopicAsync(id);
            return NoContent();
        }
    }
}
