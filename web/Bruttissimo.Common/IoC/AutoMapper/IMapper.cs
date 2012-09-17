using System;
using AutoMapper;

namespace Bruttissimo.Common
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
        IMappingExpression CreateMap(Type sourceType, Type destinationType);
        IMappingExpression CreateMap(Type sourceType, Type destinationType, MemberList source);
        IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>();
        IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>(MemberList source);
    }
}
