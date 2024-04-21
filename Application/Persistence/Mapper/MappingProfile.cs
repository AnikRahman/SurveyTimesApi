using Application.DTO;
using AutoMapper;
using Domain;

namespace Application.Persistence.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
          
            CreateMap<SurveyParticipant, SurveyParticipantDTO>();
            CreateMap<SurveyParticipantDTO, SurveyParticipant>().ReverseMap();

            CreateMap<SurveyResponse, SurveyResponseDTO>();
            CreateMap<SurveyResponseDTO, SurveyResponse>().ReverseMap();

            CreateMap<SurveyRoute, SurveyRouteDTO>();
            CreateMap<SurveyRouteDTO, SurveyRoute>().ReverseMap();
        }
    }
}
