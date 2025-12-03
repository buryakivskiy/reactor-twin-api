using AutoMapper;
using ReactorTwinAPI.Domain.Entities;
using ReactorTwinAPI.Features.ReactorTwins.Dtos;

namespace ReactorTwinAPI.Features.ReactorTwins.Mapping
{
    public class ReactorTwinProfile : Profile
    {
        public ReactorTwinProfile()
        {
            CreateMap<CreateReactorTwinDto, ReactorTwin>();
            CreateMap<UpdateReactorTwinDto, ReactorTwin>();
            CreateMap<ReactorTwin, ReactorTwinDto>();
        }
    }
}
