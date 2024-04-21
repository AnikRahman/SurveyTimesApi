

namespace Application.DTO
{
    public class SurveyResponseDTO
    {
        public int Id { get; set; }
        public int SurveyParticipantId { get; set; }
        public int SurveyRouteId { get; set; }
        public int SurveyOptionId { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
