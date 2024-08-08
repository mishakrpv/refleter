using AutoMapper;
using Storage.Application.Dtos;
using Storage.Entities.Enums;
using Storage.Entities.Models.ScopeAggregate;

namespace Storage.Application.Mapping;

public sealed class MappingProfile : Profile
{
    private static readonly Dictionary<Color, string> ColorMap = new()
    {
        { Color.Crimson, "#DC143C" },
        { Color.Orange, "#FFA500" },
        { Color.DarkKhaki, "#BDB76B" },
        { Color.Orchid, "#DA70D6" },
        { Color.Indigo, "#4B0082" },
        { Color.Lime, "#A3E600" },
        { Color.MediumSeaGreen, "#3CB371" },
        { Color.Teal, "#008080" },
        { Color.SkyBlue, "#87CEEB" },
        { Color.RoyalBlue, "#4169E1" }
    };
    
    public MappingProfile()
    {
        CreateMap<Scope, ScopeDTO>()
            .ForMember(d => d.IconColorHexCode,
                op => op.MapFrom(s => MapColor(s.Icon.Color)));
        CreateMap<Secret, SecretDTO>();
    }

    private static string MapColor(Color color)
    {
        return ColorMap[color];
    }
}