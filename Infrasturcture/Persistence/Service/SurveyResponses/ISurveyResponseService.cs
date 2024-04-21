

using Domain;
using System.Linq.Expressions;

namespace Infrasturcture.Persistence.Service.SurveyResponses
{
    public interface ISurveyResponseService
    {
        Task<IEnumerable<SurveyResponse>> GetAllSurveyResponsesAsync(params Expression<Func<SurveyResponse, object>>[] includeProperties);
        Task<SurveyResponse?> GetSurveyResponseByIdAsync(int id, params Expression<Func<SurveyResponse, object>>[] includeProperties);
        Task AddSurveyResponseAsync(SurveyResponse response);
        Task UpdateSurveyResponseAsync(SurveyResponse response);
        Task DeleteSurveyResponseAsync(int id);
    }
}
