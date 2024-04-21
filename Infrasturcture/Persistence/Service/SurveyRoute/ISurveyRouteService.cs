
using Domain;

namespace Infrasturcture.Persistence.Service
{
    public interface ISurveyRouteService
    {
        Task<IEnumerable<SurveyRoute>> GetAllSurveyRoutesAsync();
        Task<SurveyRoute> GetSurveyRouteByIdAsync(int id);
        Task AddSurveyRouteAsync(SurveyRoute route);
        Task UpdateSurveyRouteAsync(SurveyRoute route);
        Task DeleteSurveyRouteAsync(int id);
    }
}
