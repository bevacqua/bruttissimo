namespace Bruttissimo.Common
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
