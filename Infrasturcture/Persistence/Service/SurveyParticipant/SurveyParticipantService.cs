
using Application.DTO;
using Application.Persistence.Repository;
using Domain;
using Infrasturcture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrasturcture.Persistence.Service
{
    public class SurveyParticipantService : ISurveyParticipantService
    {
        private readonly IRepository<SurveyParticipant> _surveyParticipantRepository;
        private readonly IUnitOfWork _unitOfWork;
        protected readonly ApplicationDbContext _context;

        public SurveyParticipantService(IRepository<SurveyParticipant> surveyParticipantRepository, IUnitOfWork unitOfWork , ApplicationDbContext context)
        {
            _surveyParticipantRepository = surveyParticipantRepository;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IEnumerable<SurveyParticipant>> GetAllSurveyParticipantsAsync()
        {
            return await _surveyParticipantRepository.GetAllAsync();
        }

        public async Task<SurveyParticipant> GetSurveyParticipantByIdAsync(int id)
        {
            return await _surveyParticipantRepository.GetByIdAsync(id);
        }

        public async Task AddSurveyParticipantAsync(SurveyParticipant participant)
        {
            _surveyParticipantRepository.Add(participant);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateSurveyParticipantAsync(SurveyParticipant participant)
        {
            _surveyParticipantRepository.Update(participant);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteSurveyParticipantAsync(int id)
        {
            var participant = await _surveyParticipantRepository.GetByIdAsync(id);
            if (participant != null)
            {
                _surveyParticipantRepository.Remove(participant);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CombinedSurveyDataDTO>> GetAllCombinedSurveyDataAsync()
        {
            var data = await (from sp in _context.SurveyParticipants
                              join sr in _context.SurveyResponses on sp.Id equals sr.SurveyParticipantId
                              join r in _context.SurveyRoutes on sr.SurveyRouteId equals r.Id
                              join o in _context.SurveyOption on sr.SurveyOptionId equals o.Id
                              select new CombinedSurveyDataDTO
                              {
                                  SurveyParticipantId = sp.Id,
                                  FirstName = sp.FirstName,
                                  LastName = sp.LastName,
                                  Email = sp.Email,
                                  PhoneNumber = sp.PhoneNumber,
                                  Occupation = sp.Occupation,
                                  Age = sp.Age,
                                  PresentMentalState = sp.PresentMentalState,
                                  Timestamp = sr.Timestamp,
                                  Latitude = sr.Latitude,
                                  Longitude = sr.Longitude,
                                  RouteName = r.RouteName,
                                  OptionName = o.OptionName
                              }).ToListAsync();
            return data;
        }

        public async Task<IEnumerable<CombinedSurveyDataDTO>> GetByIdCombinedSurveyDataAsync(int participantId)
        {
            var data = await (from sp in _context.SurveyParticipants
                              join sr in _context.SurveyResponses on sp.Id equals sr.SurveyParticipantId
                              join r in _context.SurveyRoutes on sr.SurveyRouteId equals r.Id
                              join o in _context.SurveyOption on sr.SurveyOptionId equals o.Id
                              where sp.Id == participantId
                              select new CombinedSurveyDataDTO
                              {
                                  SurveyParticipantId = sp.Id,
                                  FirstName = sp.FirstName,
                                  LastName = sp.LastName,
                                  Email = sp.Email,
                                  PhoneNumber = sp.PhoneNumber,
                                  Occupation = sp.Occupation,
                                  Age = sp.Age,
                                  PresentMentalState = sp.PresentMentalState,
                                  Timestamp = sr.Timestamp,
                                  Latitude = sr.Latitude,
                                  Longitude = sr.Longitude,
                                  RouteName = r.RouteName,
                                  OptionName = o.OptionName
                              }).ToListAsync();
            return data;
        }
    }
}
