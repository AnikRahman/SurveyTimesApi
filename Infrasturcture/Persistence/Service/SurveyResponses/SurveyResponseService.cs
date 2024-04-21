using Application.Persistence.Repository;
using Domain;
using System.Linq.Expressions;


namespace Infrasturcture.Persistence.Service.SurveyResponses
{
    public class SurveyResponseService : ISurveyResponseService
    {
        private readonly IRepository<SurveyResponse> _surveyResponseRepository;
     
        private readonly IUnitOfWork _unitOfWork;

        public SurveyResponseService(IRepository<SurveyResponse> surveyResponseRepository, IUnitOfWork unitOfWork)
        {
            _surveyResponseRepository = surveyResponseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SurveyResponse>> GetAllSurveyResponsesAsync(params Expression<Func<SurveyResponse, object>>[] includeProperties)
        {
            return await _surveyResponseRepository.GetAllAsync(includeProperties);
        }

        public async Task<SurveyResponse?> GetSurveyResponseByIdAsync(int id, params Expression<Func<SurveyResponse, object>>[] includeProperties)
        {
            return await _surveyResponseRepository.GetByIdAsync(id, includeProperties);
        }

        public async Task AddSurveyResponseAsync(SurveyResponse response)
        {
            _surveyResponseRepository.Add(response);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateSurveyResponseAsync(SurveyResponse response)
        {
            _surveyResponseRepository.Update(response);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteSurveyResponseAsync(int id)
        {
            var response = await _surveyResponseRepository.GetByIdAsync(id);
            if (response != null)
            {
                _surveyResponseRepository.Remove(response);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
