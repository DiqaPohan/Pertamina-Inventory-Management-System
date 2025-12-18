using AutoMapper;

namespace Pertamina.SolutionTemplate.Application.Common.Mappings;

public interface IMapFrom<TSource, TDestination>
{
    void Mapping(Profile profile)
    {
        profile.CreateMap<TSource, TDestination>();
    }
}
