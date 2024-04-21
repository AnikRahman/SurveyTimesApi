

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
}
