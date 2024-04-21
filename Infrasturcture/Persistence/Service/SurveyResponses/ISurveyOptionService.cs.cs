
using Domain;

namespace Infrasturcture.Persistence.Service
{
    public interface ISurveyOptionService
    {
        Task<IEnumerable<SurveyOption>> GetAllSurveyOptionsAsync();
    }
}
