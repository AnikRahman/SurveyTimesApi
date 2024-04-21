
using Application.Persistence.Repository;
using Domain;

namespace Infrasturcture.Persistence.Service
{
    public class SurveyParticipantService : ISurveyParticipantService
    {
        private readonly IRepository<SurveyParticipant> _surveyParticipantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SurveyParticipantService(IRepository<SurveyParticipant> surveyParticipantRepository, IUnitOfWork unitOfWork)
        {
            _surveyParticipantRepository = surveyParticipantRepository;
            _unitOfWork = unitOfWork;
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
    }
}
