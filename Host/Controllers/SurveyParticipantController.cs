﻿using Application.DTO;
using AutoMapper;
using Domain;
using Infrasturcture.Persistence.Service;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Host.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SurveyParticipantController : ControllerBase
    {
        private readonly ISurveyParticipantService _surveyParticipantService;
        private readonly IMapper _mapper;

        public SurveyParticipantController(ISurveyParticipantService surveyParticipantService, IMapper mapper)
        {
            _surveyParticipantService = surveyParticipantService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyParticipantDTO>>> GetSurveyParticipants()
        {
            var surveyParticipants = await _surveyParticipantService.GetAllSurveyParticipantsAsync();
            var surveyParticipantDTOs = _mapper.Map<IEnumerable<SurveyParticipantDTO>>(surveyParticipants);
            return Ok(surveyParticipantDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyParticipantDTO>> GetSurveyParticipant(int id)
        {
            var surveyParticipant = await _surveyParticipantService.GetSurveyParticipantByIdAsync(id);

            if (surveyParticipant == null)
            {
                return NotFound();
            }

            var surveyParticipantDTO = _mapper.Map<SurveyParticipantDTO>(surveyParticipant);
            return Ok(surveyParticipantDTO);
        }

        [HttpPost]
        public async Task<ActionResult<SurveyParticipantDTO>> PostSurveyParticipant(SurveyParticipantDTO participantDTO)
        {
            try
            {
                var participant = _mapper.Map<SurveyParticipant>(participantDTO);
                await _surveyParticipantService.AddSurveyParticipantAsync(participant);
                var createdParticipantDTO = _mapper.Map<SurveyParticipantDTO>(participant);
                return CreatedAtAction(nameof(GetSurveyParticipant), new { id = createdParticipantDTO.Id }, createdParticipantDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveyParticipant(int id, SurveyParticipantDTO participantDTO)
        {
            if (id != participantDTO.Id)
            {
                return BadRequest("Invalid ID");
            }

            try
            {
                var participant = _mapper.Map<SurveyParticipant>(participantDTO);
                await _surveyParticipantService.UpdateSurveyParticipantAsync(participant);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveyParticipant(int id)
        {
            try
            {
                await _surveyParticipantService.DeleteSurveyParticipantAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("export-all")]
        public async Task<IActionResult> ExportAllSurveyParticipantsDataToExcel()
        {
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var combinedSurveyData = await _surveyParticipantService.GetAllCombinedSurveyDataAsync();

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("SurveyData");
                worksheet.Cells.LoadFromCollection(combinedSurveyData, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"SurveyData-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportByIdSurveyParticipantDataToExcel([FromQuery] int participantId)
        {

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var participantData = await _surveyParticipantService.GetByIdCombinedSurveyDataAsync(participantId);

            if (participantData == null)
            {
                return NotFound();
            }

            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add($"SurveyData_{participantId}");
                worksheet.Cells.LoadFromCollection(participantData, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"SurveyData_Participant_{participantId}-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}

