using Application.DTO;
using AutoMapper;
using Domain;
using Infrasturcture.Persistence.Service;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyRouteController : ControllerBase
    {
        private readonly ISurveyRouteService _surveyRouteService;
        private readonly IMapper _mapper;

        public SurveyRouteController(ISurveyRouteService surveyRouteService, IMapper mapper)
        {
            _surveyRouteService = surveyRouteService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyRouteDTO>>> GetSurveyRoutes()
        {
            var surveyRoutes = await _surveyRouteService.GetAllSurveyRoutesAsync();
            var surveyRouteDTOs = _mapper.Map<IEnumerable<SurveyRouteDTO>>(surveyRoutes);
            return Ok(surveyRouteDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyRouteDTO>> GetSurveyRoute(int id)
        {
            var surveyRoute = await _surveyRouteService.GetSurveyRouteByIdAsync(id);

            if (surveyRoute == null)
            {
                return NotFound();
            }

            var surveyRouteDTO = _mapper.Map<SurveyRouteDTO>(surveyRoute);
            return Ok(surveyRouteDTO);
        }

        [HttpPost]
        public async Task<ActionResult<SurveyRouteDTO>> PostSurveyRoute(SurveyRouteDTO routeDTO)
        {
            try
            {
                var route = _mapper.Map<SurveyRoute>(routeDTO);
                await _surveyRouteService.AddSurveyRouteAsync(route);
                var createdRouteDTO = _mapper.Map<SurveyRouteDTO>(route);
                return CreatedAtAction(nameof(GetSurveyRoute), new { id = createdRouteDTO.Id }, createdRouteDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveyRoute(int id, SurveyRouteDTO routeDTO)
        {
            if (id != routeDTO.Id)
            {
                return BadRequest("Invalid ID");
            }

            try
            {
                var route = _mapper.Map<SurveyRoute>(routeDTO);
                await _surveyRouteService.UpdateSurveyRouteAsync(route);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveyRoute(int id)
        {
            try
            {
                await _surveyRouteService.DeleteSurveyRouteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
