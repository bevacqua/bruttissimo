namespace Bruttissimo.Common.Mvc
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
