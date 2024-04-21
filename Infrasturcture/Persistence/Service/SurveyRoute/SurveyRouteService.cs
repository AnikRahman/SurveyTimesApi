using Application.Persistence.Repository;
using Domain;


namespace Infrasturcture.Persistence.Service
{
    public class SurveyRouteService : ISurveyRouteService
    {
        private readonly IRepository<SurveyRoute> _surveyRouteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SurveyRouteService(IRepository<SurveyRoute> surveyRouteRepository, IUnitOfWork unitOfWork)
        {
            _surveyRouteRepository = surveyRouteRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SurveyRoute>> GetAllSurveyRoutesAsync()
        {
            return await _surveyRouteRepository.GetAllAsync();
        }

        public async Task<SurveyRoute> GetSurveyRouteByIdAsync(int id)
        {
            return await _surveyRouteRepository.GetByIdAsync(id);
        }

        public async Task AddSurveyRouteAsync(SurveyRoute route)
        {
            _surveyRouteRepository.Add(route);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateSurveyRouteAsync(SurveyRoute route)
        {
            _surveyRouteRepository.Update(route);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteSurveyRouteAsync(int id)
        {
            var route = await _surveyRouteRepository.GetByIdAsync(id);
            if (route != null)
            {
                _surveyRouteRepository.Remove(route);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
