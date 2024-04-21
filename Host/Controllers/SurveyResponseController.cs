
using Application.DTO;
using AutoMapper;
using Domain;
using Infrasturcture.Persistence.Service;
using Infrasturcture.Persistence.Service.SurveyResponses;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyResponseController : ControllerBase
    {
        private readonly ISurveyResponseService _surveyResponseService;
        private readonly ISurveyOptionService _surveyOptionService;  // Add this line
        private readonly IMapper _mapper;

        public SurveyResponseController(ISurveyResponseService surveyResponseService, ISurveyOptionService surveyOptionService, IMapper mapper)
        {
            _surveyResponseService = surveyResponseService;
            _surveyOptionService = surveyOptionService;  // Add this line
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyResponse>>> GetSurveyResponses()
        {
            var responses = await _surveyResponseService.GetAllSurveyResponsesAsync(r => r.SurveyParticipant, r => r.SurveyRoute, r => r.SurveyOption);
            return Ok(responses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyResponse>> GetSurveyResponse(int id)
        {
            var response = await _surveyResponseService.GetSurveyResponseByIdAsync(id, r => r.SurveyParticipant, r => r.SurveyRoute, r => r.SurveyOption);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<SurveyResponseDTO>> PostSurveyResponse(SurveyResponseDTO responseDTO)
        {
            try
            {
                var response = _mapper.Map<SurveyResponse>(responseDTO);
                await _surveyResponseService.AddSurveyResponseAsync(response);
                var createdResponseDTO = _mapper.Map<SurveyResponseDTO>(response);
                return CreatedAtAction(nameof(GetSurveyResponse), new { id = createdResponseDTO.Id }, createdResponseDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveyResponse(int id, SurveyResponseDTO responseDTO)
        {
            if (id != responseDTO.Id)
            {
                return BadRequest("Invalid ID");
            }

            try
            {
                var response = _mapper.Map<SurveyResponse>(responseDTO);
                await _surveyResponseService.UpdateSurveyResponseAsync(response);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveyResponse(int id)
        {
            try
            {
                await _surveyResponseService.DeleteSurveyResponseAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("options")]
        public async Task<ActionResult<IEnumerable<SurveyOption>>> GetSurveyOptions()
        {
            var options = await _surveyOptionService.GetAllSurveyOptionsAsync();
            if (options == null || !options.Any())
            {
                return NotFound("No survey options found.");
            }
            return Ok(options);
        }


    }
}
