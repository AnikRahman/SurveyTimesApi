

namespace Application.DTO
{
    public class SurveyParticipantDTO

    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Occupation { get; set; }
        public int Age { get; set; }
        public string? PresentMentalState { get; set; }
    }


    public class CombinedSurveyDataDTO
    {
        public int SurveyParticipantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Occupation { get; set; }
        public int Age { get; set; }
        public string PresentMentalState { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string RouteName { get; set; }
        public string OptionName { get; set; }
    }
}
