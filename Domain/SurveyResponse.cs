using Domain;

public class SurveyResponse
{
    public int Id { get; set; }
    public int SurveyParticipantId { get; set; }
    public int SurveyRouteId { get; set; }
    public int SurveyOptionId { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }

    // Navigation properties
    public SurveyParticipant? SurveyParticipant { get; set; }
    public SurveyRoute? SurveyRoute { get; set; }
    public SurveyOption? SurveyOption { get; set; }
}