using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile 
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            // Walk Mappings
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<WalkDto, AddWalkRequestDto>().ReverseMap();
            CreateMap<WalkDto, UpdateWalkRequestDto>().ReverseMap();
            CreateMap<Walk, AddWalkRequestDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkRequestDto>().ReverseMap();
            // Difficulty Mappings
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
        }
    }
}
