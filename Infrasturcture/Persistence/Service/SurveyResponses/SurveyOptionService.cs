

using Application.Persistence.Repository;
using Domain;

namespace Infrasturcture.Persistence.Service
{
    public class SurveyOptionService : ISurveyOptionService
    {
        private readonly IRepository<SurveyOption> _surveyOptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SurveyOptionService(IRepository<SurveyOption> surveyOptionRepository, IUnitOfWork unitOfWork)
        {
            _surveyOptionRepository = surveyOptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SurveyOption>> GetAllSurveyOptionsAsync()
        {
            return await _surveyOptionRepository.GetAllAsync();
        }
    }
}
