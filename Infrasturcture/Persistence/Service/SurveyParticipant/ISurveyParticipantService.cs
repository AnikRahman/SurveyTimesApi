
using Application.DTO;
using Domain;

namespace Infrasturcture.Persistence.Service
{
    public interface ISurveyParticipantService
    {
        Task<IEnumerable<SurveyParticipant>> GetAllSurveyParticipantsAsync();
        Task<SurveyParticipant> GetSurveyParticipantByIdAsync(int id);
        Task AddSurveyParticipantAsync(SurveyParticipant participant);
        Task UpdateSurveyParticipantAsync(SurveyParticipant participant);
        Task DeleteSurveyParticipantAsync(int id);
        Task<IEnumerable<CombinedSurveyDataDTO>> GetAllCombinedSurveyDataAsync();
        Task<IEnumerable<CombinedSurveyDataDTO>> GetByIdCombinedSurveyDataAsync(int participantId);
    }
}
